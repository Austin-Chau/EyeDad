using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindCamera : MonoBehaviour {

    private GameObject[] FOVs;

    //Code below finds all FOVs paired with enemies and disables them for player.
    //This is only necessary if walls and floor are currently visible.

    private void Start()
    {
        FOVs = GameObject.FindGameObjectsWithTag("FOV");
    }

    private void OnPreCull()
    {
        for (int i = 0; i < FOVs.Length; i++)
        {
            FOVs[i].GetComponent<Light>().enabled = false;
        }
    }
    
    private void OnPreRender()
    {
        for(int i = 0; i < FOVs.Length; i++)
        {
            FOVs[i].GetComponent<Light>().enabled = false;
        }
    }

    private void OnPostRender()
    {
        for (int i = 0; i < FOVs.Length; i++)
        {
            FOVs[i].GetComponent<Light>().enabled = true;
        }
    }
}
