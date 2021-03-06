using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_inter : MonoBehaviour
{

    public Vector3 Axis = new Vector3(0, -1, 0);
    public float MaxDistance;
    public float ReturnSpeed = 10.0f;

    Vector3 m_StartPosition;
    Rigidbody m_Rigidbody;
    Collider m_Collider;
    bool m_Pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponentInChildren<Collider>();
        m_StartPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 worldAxis = transform.TransformDirection(Axis);
        Vector3 end = transform.position + worldAxis * MaxDistance;

        float m_CurrentDistance = (transform.position - m_StartPosition).magnitude;
        RaycastHit info;

        float move = 0.0f;

        if (m_Rigidbody.SweepTest(-worldAxis, out info, ReturnSpeed * Time.deltaTime + 0.005f))
        {//hitting something, if the contact is < mean we are pressed, move downward
            move = (ReturnSpeed * Time.deltaTime) - info.distance;
        }
        else
        {
            move -= ReturnSpeed * Time.deltaTime;
        }

        float newDistance = Mathf.Clamp(m_CurrentDistance + move, 0, MaxDistance);

        m_Rigidbody.position = m_StartPosition + worldAxis * newDistance;

        if (!m_Pressed && Mathf.Approximately(newDistance, MaxDistance))
        {//was just pressed
            m_Pressed = true;
            //SFXPlayer.Instance.PlaySFX(ButtonPressAudioClip, transform.position, new SFXPlayer.PlayParameters()
            //{
            //    Pitch = Random.Range(0.9f, 1.1f),
            //    SourceID = -1,
            //    Volume = 1.0f
            //}, 0.0f);
            //OnButtonPressed.Invoke();
        }
        else if (m_Pressed && !Mathf.Approximately(newDistance, MaxDistance))
        {//was just released
            m_Pressed = false;
            //SFXPlayer.Instance.PlaySFX(ButtonReleaseAudioClip, transform.position, new SFXPlayer.PlayParameters()
            //{
            //    Pitch = Random.Range(0.9f, 1.1f),
            //    SourceID = -1,
            //    Volume = 1.0f
            //}, 0.0f);
            //OnButtonReleased.Invoke();
        }
    }
}
