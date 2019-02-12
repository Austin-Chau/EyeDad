using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ControllerScript : MonoBehaviour {

    public SteamVR_Action_Boolean pinch;
    public SteamVR_Action_Vibration vibrate;
    public GameObject model;
    public float throwStrength = 400;
    public Material defaultMat;

    private Material material;
    private SteamVR_Behaviour_Pose bp;
    public bool isHolding = false;

    private void Start()
    {
        bp = GetComponent<SteamVR_Behaviour_Pose>();
        model = transform.Find("Model").gameObject;
        
    }


    private void Update()
    {
        if (pinch.GetStateUp(bp.inputSource) && isHolding){
            foreach (Transform child in this.transform)
            {
                //Checks each child in controller to see if any object is picked up and drops it
                if (child.tag == "Ammo")
                {
                    child.transform.parent = GameObject.Find("Baseballs").transform;
                    child.GetComponent<Rigidbody>().detectCollisions = true;
                    child.GetComponent<Rigidbody>().useGravity = true;
                    child.GetComponent<Rigidbody>().AddForce(new Vector3(bp.GetVelocity().x * throwStrength, bp.GetVelocity().y * 50, bp.GetVelocity().z * throwStrength ));
                    isHolding = false;
                    Destroy(child.gameObject, 5.0f);
                }
            }
        }
        /*if (model.transform.Find("trigger") != null)
        {
            model.transform.Find("trigger").GetComponent<MeshRenderer>().material = defaultMat;
            model.transform.Find("trigger").GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0, 0, 1, 1));
        }*/
    }

    private void OnTriggerStay(Collider other)
    {
        
        float distance = Vector3.Distance(this.transform.position, other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
        if (distance < 1)
        {
            if (other.tag == "Vendor" && pinch.GetStateDown(bp.inputSource) && !isHolding && distance < .3)
            {
                GameObject ammo = Instantiate(other.GetComponent<VendorScript>().ammo);
                ammo.transform.parent = this.transform;
                ammo.transform.position = this.transform.position;
                ammo.GetComponent<Rigidbody>().detectCollisions = false;
                isHolding = true;
            }
            float i;
            /*if (1 - distance < .5)
                i = .5f;
            else*/
            i = 1 - distance;
            material.SetColor("_TintColor", new Color(1, 0, 0, i));
            if (distance <= 0 && vibrate != null) vibrate.Execute(0, .2f, 20, .7f, bp.inputSource);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        material = GetComponentInChildren<MeshRenderer>().material;
    }

    private void OnTriggerExit(Collider other)
    {
        material.SetColor("_TintColor", new Color(1, 0, 0, 0));
    }
}
