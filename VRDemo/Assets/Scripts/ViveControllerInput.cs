using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ViveControllerInput : MonoBehaviour {
    public SteamVR_Action_Vector2 moveAction;

    public GameObject cam;

    public float speed = 100f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        //Management of Input to movement
        //Changes the x,y coordinates of the touchpad to x,z for unity space.
        Vector3 touchPadInput = moveAction.GetAxis(SteamVR_Input_Sources.Any);
        Vector3 coordinateShift = new Vector3(touchPadInput.x, 0, touchPadInput.y);
        //Normalizes for consistent speed regardless of distance from origin 
        Vector3 direction = Vector3.Normalize(coordinateShift);
        //Points direction vector relative to camera
        Quaternion rotation = Quaternion.LookRotation(cam.transform.forward, cam.transform.up);

        rb.velocity = rotation * direction * speed * Time.deltaTime;
    }

}
