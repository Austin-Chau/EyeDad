using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoorScript : MonoBehaviour {
    //I regret not doing this first
    public Director.Direction directionMoved;
    public float doorSpeed = 1;

    private Director director = new Director();
    private Vector3 doorOrigin;
    private Vector3 buttonOrigin;

    protected bool triggerActivated = false;
    protected float timeStart;


    private void Update()
    {
        director.direction = directionMoved;
        Vector3 dir = director.GetVectorDirection();
        
        if (triggerActivated && Time.time - timeStart < GetLocalScale(gameObject) / doorSpeed)
            transform.Translate(dir * Time.deltaTime * doorSpeed);
    }

    public void StartMoving()
    {
        timeStart = Time.time;
        triggerActivated = true;
    }


    private float GetLocalScale(GameObject door)
    {
        switch (director.GetAxis())
        {
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
