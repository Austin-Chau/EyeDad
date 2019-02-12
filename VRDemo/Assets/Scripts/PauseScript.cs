using UnityEngine;

public class PauseScript : MonoBehaviour {

    private GameObject AICam;
    private Canvas canvas;
	// Use this for initialization
	void Start () {
        AICam = GameObject.Find("AITopDown");
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = AICam.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
