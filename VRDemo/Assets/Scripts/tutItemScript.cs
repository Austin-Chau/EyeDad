using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutItemScript : MonoBehaviour
{

    private GameObject tutorial;
    private bool entered = false;
    public int seqNum;

    // Use this for initialization
    void Start()
    {
        tutorial = GameObject.Find("TutorialManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!entered && other.tag != "Tutorial")
        {
            tutorial.GetComponent<TutorialScript>().audioActivate(seqNum);
            entered = true;
        }

    }
}