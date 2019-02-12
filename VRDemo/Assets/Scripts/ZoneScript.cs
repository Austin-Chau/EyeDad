using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneScript : DoorButtonScript{

    [Header("Enemy Object to start movement")]
    public GameObject enemy;
    public int movementDelay = 2;
    public bool isCheckpoint = false;

    private GameObject leftController;
    private GameObject rightController;
    private GameObject tutorialManager;
    private bool doorMoved = false;

    public override void Start()
    {
        leftController = GameObject.Find("Controller (left)");
        rightController = GameObject.Find("Controller (right)");
        tutorialManager = GameObject.Find("TutorialManager");
        base.Start();
    }

    public override void Update()
    {
        if (enemy != null && triggerActivated)
        {
            StartCoroutine(SetEnemyMove());
        }
        if (door != null && !doorMoved)
        {
            Vector3 dir = director.GetVectorDirection();
            if (triggerActivated && Time.time - timeStart < GetLocalScale(door) / doorSpeed)
                door.transform.Translate(dir * Time.deltaTime * doorSpeed);
        }
        if (triggerActivated)
        {
            if (Time.time - timeStart > GetLocalScale(door) / doorSpeed)
                doorMoved = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCamera") { 
            timeStart = Time.time;
            triggerActivated = true;
            
            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Ammo"))
            {
                Destroy(go);
            }

            leftController.GetComponent<ControllerScript>().isHolding = false;
            rightController.GetComponent<ControllerScript>().isHolding = false;

            if(isCheckpoint) tutorialManager.GetComponent<TutorialScript>().SetSpawn(transform.position);

            GetComponent<Collider>().enabled = false;
        }
    }

    IEnumerator SetEnemyMove()
    {
        yield return new WaitForSeconds(movementDelay);
        enemy.GetComponent<EnemyScript>().moving = true;
        gameObject.SetActive(false);
    }
}
