using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonScript : MonoBehaviour {
    
    

    // Use this for initialization
    public GameObject door;
    public Director.Direction directionMoved;
    public float doorSpeed = 1;

    protected Director director = new Director();
    private Vector3 doorOrigin;
    private Vector3 buttonOrigin;

    protected bool triggerActivated = false;
    protected float timeStart;

    public virtual void Start()
    {
        //creates a Director object to handle directions using enums
        director.direction = directionMoved;

        //Save the position for reset on death.
        doorOrigin = door.transform.position;
        buttonOrigin = transform.position;
    }

    public virtual void Update()
    {
        if (door.scene.IsValid())
        {
            Vector3 dir = director.GetVectorDirection();
            if (triggerActivated && Time.time - timeStart < GetLocalScale(door) / doorSpeed)
                door.transform.Translate(dir * Time.deltaTime * doorSpeed);
        }
        if (triggerActivated)
        {
            if (Time.time - timeStart > GetLocalScale(door) / doorSpeed)
                gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Controller")
        {
            timeStart = Time.time;
            triggerActivated = true;
            GetComponent<Collider>().enabled = false;
        }
    }

    public void ResetThis()
    {
        if(door != null)
            door.transform.position = doorOrigin;
        transform.position = buttonOrigin;
        triggerActivated = false;
    }

    protected float GetLocalScale(GameObject door) {
        switch (director.GetAxis()) {

            case "x":
                return door.transform.localScale.x;
            case "y":
                return door.transform.localScale.y;
            case "z":
                return door.transform.localScale.z;
            default:
                throw new System.ArgumentException("Direction was not given.");
        }   
    }

}
