using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TorchTipHandler : MonoBehaviour
{
    //public GameObject[] Fire_Collider; //All fire colliders
    public ParticleSystem[] torchFire;
    public ParticleSystem[] webFire;
    public GameObject key;
    public GameObject spiderWeb;
    public GameObject ourTorchLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("FireCollider"))
        {
            foreach (ParticleSystem fire in torchFire)
            {
                fire.Play();

            }
            ourTorchLight.SetActive(true);
        }
        if (other.tag.Equals("SpiderWeb") && torchFire[0].isPlaying && !webFire[0].isPlaying)
        {
            foreach (ParticleSystem fire in webFire)
            {
                fire.Play();
            }
            Invoke("Delay", 1.5f);
        }

    }

    void Delay()
    {
        spiderWeb.SetActive(false);
        //NEW
        key.GetComponent<Rigidbody>().isKinematic = false;
        //End_New
        key.GetComponent<Rigidbody>().useGravity = true;
        key.GetComponent<XRGrabInteractable>().enabled = true;
    }
}
