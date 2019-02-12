using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoScript : MonoBehaviour {

    public AudioClip breakLineBeginning;
    public AudioClip[] breakLines;
    public AudioClip[] breakSounds;

    private GameObject parent;
    private bool firstHit = true;

    void Update () {
        if(transform.parent.tag == "Controller")
            transform.position = transform.parent.transform.position;
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Enemy" && GetComponent<Rigidbody>().velocity.magnitude > .5 && firstHit)
        {

            firstHit = false;
            GameObject go = new GameObject("AudioSource");
            GameObject camera = GameObject.Find("Camera");

            go.transform.parent = camera.transform;

            AudioSource audio = go.AddComponent<AudioSource>() as AudioSource;
            Debug.Log("Created Audio Source");
            parent = transform.parent.gameObject;
            if (!parent.GetComponent<Baseballs>().isSpeaking)
                StartCoroutine(playLines(audio, go));
            else
                StartCoroutine(playSounds(audio, go));
        }
        else if (collision.collider.tag == "Enemy") firstHit = false;
    }



    IEnumerator playLines(AudioSource audio, GameObject go)
    {
        Debug.Log("Playing a Line");
        parent.GetComponent<Baseballs>().isSpeaking = true;
        audio.clip = breakLineBeginning;
        audio.Play();
        yield return new WaitForSeconds(breakLineBeginning.length);

        AudioClip ac = breakLines[Random.Range(0, breakLines.Length - 1)];
        audio.clip = ac;
        audio.Play();

        yield return new WaitForSeconds(ac.length);
        parent.GetComponent<Baseballs>().isSpeaking = false;

        Destroy(go);
        Destroy(audio);
    }

    IEnumerator playSounds(AudioSource audio, GameObject go)
    {
        Debug.Log("Playing a Sound");
        AudioClip ac = breakSounds[Random.Range(0, breakSounds.Length - 1)];
        audio.clip = ac;
        audio.Play();

        yield return new WaitForSeconds(ac.length);

        Destroy(audio);
        Destroy(go);
    }


}
