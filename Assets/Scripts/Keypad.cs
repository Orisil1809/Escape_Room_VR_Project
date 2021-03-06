using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Keypad : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;
    public GameObject FinalTeleport;

    public UnityEvent onPressed, onReleased;

    private AudioSource fountainSound;
    private bool isPressed;
    private Vector3 startPos;
    private ConfigurableJoint joint;

    private Animator fountainAnimator;
    private GameObject fountain;

    private GameObject userPassword;
    private string password = "925";
    public Data_Log ourDataLog;

    // Start is called before the first frame update
    void Start()
    {
        fountain = GameObject.Find("Fountain_01");
        fountainAnimator = fountain.GetComponent<Animator>();
        fountainSound = fountain.GetComponent<AudioSource>();
        userPassword = GameObject.Find("Input_menu_obj");
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
        }

        if (isPressed && GetValue() - threshold <= 0)
        {
            Released();
        }
    }

    private float GetValue()
    {
        var value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;
        if (Mathf.Abs(value) < deadZone)
        {
            value = 0;
        }
        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        isPressed = true;

        if (transform.tag.Equals("number") && !(userPassword.GetComponent<UnityEngine.UI.Text>().text.Equals("Correct")) && userPassword.GetComponent<UnityEngine.UI.Text>().text.Length < 5)
        {
            userPassword.GetComponent<UnityEngine.UI.Text>().text += transform.name;
        }
        if(transform.name.Equals("Reset"))
        {
            ClearButtonPushed();
        }
        if(transform.name.Equals("Accept"))
        {
            VerifyButtonPushed();
        }
        onPressed.Invoke();
        //Debug.Log("is pressed");

    }

    private void Released()
    {
        isPressed = false;
        onPressed.Invoke();
        //Debug.Log("is released");

    }

    void ClearButtonPushed()
    {
        userPassword.GetComponent<UnityEngine.UI.Text>().text = "";

    }

    void VerifyButtonPushed()
    {
        if (password.Equals(userPassword.GetComponent<UnityEngine.UI.Text>().text)) //Mission 3 completed
        {
            ourDataLog.trial["riddle3"] = (Time.time - ourDataLog.startSceneTime).ToString();
            FinalTeleport.SetActive(true);
            userPassword.GetComponent<UnityEngine.UI.Text>().text = "Correct";
            fountainAnimator.SetTrigger("fountainTrigger");
            fountainSound.Play();
            //correct password
        }
        else
        {
            //incorrect password
            ClearButtonPushed();
        }
    }

}