using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICameraScript : MonoBehaviour {

    // Use this for initialization
    public float switchCooldown = 1;
    public Canvas canvas;

    private Camera FPcam;
    public Camera TPcam;
    private float horizontal;
    private float vertical;
    private float switchTime = 0;

    private void Start()
    {
        FPcam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (FPcam.enabled && Time.time - switchTime > switchCooldown)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = -Input.GetAxis("Vertical");
            transform.Translate(new Vector3(vertical, 0, horizontal), Space.World);
        }
        if (Input.GetButtonDown("SwitchCamera") && Time.time - switchTime > switchCooldown)
        {
            SwitchCamera();
        }
    }

    public void SwitchCamera()
    {
        if (FPcam.enabled)
        {
            FPcam.enabled = false;
            TPcam.enabled = true;
            canvas.worldCamera = TPcam;
        }
        else
        {
            Vector3 playerPos = GameObject.Find("Player").transform.position;
            transform.position = new Vector3(playerPos.x, transform.position.y, playerPos.z);
            FPcam.enabled = true;
            TPcam.enabled = false;
            canvas.worldCamera = FPcam;
        }
        switchTime = Time.time;
    }


}
