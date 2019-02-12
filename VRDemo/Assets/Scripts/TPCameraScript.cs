using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCameraScript : MonoBehaviour {

    public float turnSpeed = 4.0f;
    public float distance = 3;

    private Transform player;
    private Camera camera;
    private Vector3 offset;


	// Use this for initialization
	void Start () {
        camera = GetComponent<Camera>();
        player = GameObject.Find("Player").transform;
        offset = new Vector3(distance, transform.position.y, distance);
    }
	
	// Update is called once per frame
	void Update () {
        if (camera.enabled)
        {
            //https://answers.unity.com/questions/600577/camera-rotation-around-player-while-following.html
            offset = Quaternion.AngleAxis(Input.GetAxis("Horizontal") * turnSpeed, Vector3.up) * offset;
            transform.position = player.position + offset;
            transform.LookAt(player.position);
        }
	}
}
