using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    public GameObject sceneManager;
    public Slider slider;

    // Update is called once per frame
    private void Start()
    {
        GetComponent<RectTransform>().localScale = new Vector3(.003f, .003f, .003f);
    }

    void Update () {
        transform.position = transform.parent.position;
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + .03f, transform.localPosition.z - .1f);
        transform.rotation = transform.parent.rotation;
        transform.rotation = Quaternion.AngleAxis(90, transform.parent.transform.up) * transform.rotation;

        slider.GetComponent<Slider>().value = sceneManager.GetComponent<SceneScript>().health;
	}
}
