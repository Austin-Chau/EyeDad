using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour {

    public const int MaxHealth = 100;

    public Button unpauseButton;
    public Text AIText;
    public Text botText;
    public Material copiedMaterial;

    public int health;

    private int gridMultiplier = 4;
    static private bool pause = true;
    private bool isDead = false;
    private GameObject PauseSceneRoot;
    private string AIDefault;
    private string botDefault;


    //https://answers.unity.com/questions/1255842/pause-scene.html
    void Start()
    {
        health = MaxHealth;
        SceneManager.LoadScene("PauseScene", LoadSceneMode.Additive);

        PauseSceneRoot = GameObject.FindWithTag("PauseMenu"); //Have all your pause scene objects, inside one object with the tag "PauseMenu" or this won't work
        PauseSceneRoot.SetActive(false);

        AIDefault = AIText.text;
        botDefault = botText.text;
        //StartCoroutine(ReskinAllObstacles());
    }

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }

            if (health < 0)
            {
                isDead = true;
                DeathPause();
            }
        }
    }

    public void TogglePause()
    {
        pause = !pause;

        Time.timeScale = (pause) ? 1.00f : 0.00f; //If your time scale isn't working, change Paused to !Paused
        PauseSceneRoot.SetActive(!pause);
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scene1");
        unpauseButton.gameObject.SetActive(true);
        AIText.gameObject.SetActive(false);
        botText.gameObject.SetActive(false);
        AIText.text = AIDefault;
        botText.text = botDefault;
    }

    public void DeathPause()
    {
        TogglePause();
        unpauseButton.gameObject.SetActive(false);
        AIText.gameObject.SetActive(true);
        botText.gameObject.SetActive(true);
    }

    public void WinPause()
    {
        AIText.text = "You Won!";
        botText.text = "You Won!";
        DeathPause();
    }

    public IEnumerator ReskinAllObstacles()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            Reskin(obstacle);
        }
        yield return null;
    }

    private void Reskin(GameObject obstacle)
    {
        bool xLong;

        if (obstacle.transform.localScale.x > obstacle.transform.localScale.z)
            xLong = true;
        else
            xLong = false;

        Material material = new Material(copiedMaterial);
        if (xLong) {
            material.SetTextureScale("_MainTex", new Vector2(obstacle.transform.localScale.x * gridMultiplier, obstacle.transform.localScale.y * gridMultiplier));
        } else
            material.SetTextureScale("_MainTex", new Vector2(obstacle.transform.localScale.z * gridMultiplier, obstacle.transform.localScale.y * gridMultiplier));
        obstacle.GetComponent<Renderer>().material = material;
    }
}
