using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour {

    public AudioSource playerAudioSource;

    public AudioClip endClip;

    public GameObject sceneManager;

    bool hasEntered = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !hasEntered)
        {
            hasEntered = true;
            StartCoroutine(EndGame());
        }
    }

    IEnumerator EndGame()
    {
        playerAudioSource.clip = endClip;
        playerAudioSource.Play();
        yield return new WaitForSeconds(endClip.length);
        sceneManager.GetComponent<SceneScript>().WinPause();

    }
}
