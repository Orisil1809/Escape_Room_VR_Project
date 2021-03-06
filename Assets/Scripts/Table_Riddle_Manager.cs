using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class Table_Riddle_Manager : MonoBehaviour
{
    public Animator bridgeAnimator;
    public Obj_Data SO;

    private XRBaseInteractable current_interactable2;
    private XRSocketInteractor current_socket;
    private string currentTag;
    public Text our_UI;
    public GameObject teleport1;
    public GameObject teleport2;
    public Data_Log ourDataLog;
    public AudioSource bridgeSound;
    //private bool reachedGoal = false;

    //public GameObject[] go_arr; //GameObject array
    //private XRSocketInteractor socket_arr;

    // Start is called before the first frame update
    void Start()
    {
        SO.Init();
        current_socket = GetComponent<XRSocketInteractor>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addWeight()
    {
        current_interactable2 = this.GetComponent<XRSocketInteractor>().selectTarget;
        if (current_interactable2 != null)
        {
            currentTag = current_interactable2.tag;
            SO.updateWeight(SO.weight_dict[currentTag]);
        }
        our_UI.text = SO.currentWeight.ToString();
        checkGoal();
    }

    public void removeWeight()
    {

        //current_interactable2 = this.GetComponent<XRSocketInteractor>().selectTarget;

        if (current_interactable2 != null)
        {
            currentTag = current_interactable2.tag;
            SO.updateWeight(-SO.weight_dict[currentTag]);
        }

        current_interactable2 = this.GetComponent<XRSocketInteractor>().selectTarget;
        checkGoal();

        our_UI.text = SO.currentWeight.ToString();
    }

    private void checkGoal()
    {
        if (SO.currentWeight == SO.goalWeight && bridgeAnimator != null && !SO.goalReached) //Mission 4 completed
        {
            if (ourDataLog.trial != null)
            {
                ourDataLog.trial["riddle4"] = (Time.time - ourDataLog.startSceneTime).ToString();
            }
            SO.goalReached = true;
            bridgeAnimator.SetTrigger("Bridge_anim");
            bridgeSound.Play();
            Invoke("DelaySetActive", 4f);
            //teleport1.SetActive(true);
            //teleport2.SetActive(true); 
            //Debug.Log("EQUAL");
        }
    }

    private void DelaySetActive() //Set Active the bridge teleports with delay
    {
        teleport1.SetActive(true);
        teleport2.SetActive(true);
    }
}
