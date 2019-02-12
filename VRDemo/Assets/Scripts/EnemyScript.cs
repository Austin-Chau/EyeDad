using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public GameObject enemyBullet;

    [Header("Stats")]
    public float fireRate = 1;
    public float bulletSpeed = 20;
    public float delayTime = 3;

    [Header("Movement")]
    public bool moving = true;
    public float movementSpeed = 1;
    public GameObject[] patrolPoints;

    [Header("Audio")]
    public AudioClip[] hitClips;
    public AudioClip fireClip;

    private float timeSighted = 0;
    private float lastFired = 0;
    private bool playerSighted;
    private Vector3 lastSightedPosition;
    private AudioSource audio;

    private int movementCounter = 0;

    private Quaternion startRotation;

    public void Start()
    {
        startRotation = Quaternion.LookRotation(transform.forward);
        audio = GetComponent<AudioSource>();
    }

    public void Update()
    {
        //Checks every frame if player is within enemy FOV.
        if (!playerSighted)
            moveEnemy();
        else if (playerSighted)
            attackPlayer();
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "PlayerCamera") {
            //If Player is in enemy collider then this triggers
            //Checks for the direction between the player and the enemy
            Vector3 playerHeadPosition = new Vector3(transform.position.x, transform.position.y + .7f, transform.position.z);
            Vector3 direction = Vector3.Normalize(other.transform.position - playerHeadPosition);
            lastSightedPosition = other.transform.position;

            //Raycasts to see if player is within line of sight
            RaycastHit[] hits;

            

            hits = Physics.RaycastAll(playerHeadPosition, direction, 10);

            //Since Raycasting does not return in any particular order, we must check 
            //which object is closest to the enemy. If there is an obstacle between the
            //player and the enemy, enemy does nothing. If player is the the closest,
            //the playerSighted boolean becomes true.
            float closestObjectDistance = float.MaxValue;
            float closestPlayerDistance = float.MaxValue;

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];

                if (hit.collider.tag == "Obstacle")
                {
                    if (hit.distance < closestObjectDistance)
                        closestObjectDistance = hit.distance;
                }
                else if (hit.collider.tag == "PlayerCamera")
                    if (hit.distance < closestPlayerDistance)
                        closestPlayerDistance = hit.distance;
            }

            if (closestObjectDistance > closestPlayerDistance)
            {
                float dot = Vector3.Dot(direction, this.transform.forward);
                if (dot > .01)
                {
                    //If target is within 90 degrees los
                    this.transform.forward = new Vector3(direction.x, 0, direction.z);
                    if (!playerSighted) timeSighted = Time.time;
                    playerSighted = true;
                }
            }
            else { playerSighted = false; }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playerSighted = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if(collider.tag == "Ammo")
            StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        AudioClip hitClip = hitClips[Random.Range(0, hitClips.Length - 1)];
        audio.clip = hitClip;
        audio.Play();
        yield return new WaitForSeconds(hitClip.length);
        gameObject.SetActive(false);
    }

    //https://forum.unity.com/threads/ignore-y-axis-difference-in-vector3-distance-calc-execution-time.335796/
    //Simple function that only checks the x and z distance
    private float vector2DDistance(Vector3 v1, Vector3 v2)
    {
        float xDiff = v1.x - v2.x;
        float zDiff = v1.z - v2.z;
        return Mathf.Sqrt((xDiff * xDiff) + (zDiff * zDiff));
    }

    private void attackPlayer()
    {
        //Checks the delay time and fire rate before attacking every frame
        if(Time.time - lastFired > fireRate && Time.time - timeSighted > delayTime)
        {
            //bullet is created from the enemy's head and fires towards player with correct rotation.
            GameObject go = Instantiate(enemyBullet);
            Vector3 fireSource = new Vector3(transform.position.x, transform.position.y + .7f, transform.position.z);
            go.transform.position = fireSource;
            go.GetComponent<Rigidbody>().velocity = Vector3.Normalize(lastSightedPosition - fireSource) * bulletSpeed;
            go.transform.rotation = Quaternion.LookRotation(transform.forward);


            audio.clip = fireClip;
            audio.Play();
            lastFired = Time.time;
        }
    }

    private void moveEnemy()
    {
        if (moving)
        {
            Vector3 patrolDirection = patrolPoints[movementCounter].transform.position - transform.position;
            patrolDirection = new Vector3(patrolDirection.x, 0, patrolDirection.z);
            GetComponent<Rigidbody>().velocity = Vector3.Normalize(patrolDirection) * movementSpeed;
            transform.forward = patrolDirection;
            if (vector2DDistance(patrolPoints[movementCounter].transform.position, transform.position) < .1)
            {
                movementCounter = (movementCounter + 1) % patrolPoints.Length;
            }
        }
        else
        {
            transform.rotation = startRotation;
        }
    }
}
