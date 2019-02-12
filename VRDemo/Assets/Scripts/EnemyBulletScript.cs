using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBulletScript : MonoBehaviour {

    public int damage = 40;

    private GameObject sceneManager;
    private GameObject imageObject;
    private Image image;
    private float alpha;
    private bool isFading = false;
    private AudioSource audio;

    private void Start()
    {
        imageObject = GameObject.Find("HitCover");
        sceneManager = GameObject.Find("SceneManager");
        image = imageObject.GetComponent<Image>();
        audio = GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerCamera")
        {
            if (isFading)
            {
                StopAllCoroutines();
                isFading = false;
            }
            audio.Play();
            alpha = 1;
            sceneManager.GetComponent<SceneScript>().health -= 30;
            StartCoroutine(Fade(image));
        }
        GetComponent<MeshRenderer>().enabled = false;
    }

    IEnumerator Fade(Image image)
    {
        isFading = true;
        while(alpha >= 0)
        {
            yield return new WaitForSeconds(.05f);
            image.color = new Color(1, 0, 0, alpha);
            alpha -= .05f;
        }
        image.color = new Color(1, 0, 0, 0);
        isFading = false;
        Destroy(gameObject);
    }
}
