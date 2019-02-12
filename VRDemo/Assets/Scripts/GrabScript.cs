using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabScript : MonoBehaviour {

    private bool collided;

    private void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        collided = false;
    }

    private void Update()
    {

    }
}
