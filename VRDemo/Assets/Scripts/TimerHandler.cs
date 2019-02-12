using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerHandler : MonoBehaviour {

    public float StartTimer = 999999999999;

    public GameObject sceneManager;
    public Text controlTimer;
    public Text AITimer;

    private float time;

    private void Start()
    {
        time = StartTimer;
        StartCoroutine(TimeCountdown());
    }

    IEnumerator TimeCountdown()
    {
        while(time > 0)
        {
            time -= .1f;
            time = Mathf.Round(time * 10) / 10;
            controlTimer.text = time.ToString();
            AITimer.text = time.ToString();
            if (time <= 0)
                sceneManager.GetComponent<SceneScript>().DeathPause();
            yield return new WaitForSeconds(.1f);
        }
    }
}
