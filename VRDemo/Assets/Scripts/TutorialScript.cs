using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    
    public GameObject VoiceOverObject;
    public GameObject sceneManager;
    public AudioClip[] tutorialLines = new AudioClip[9];
    
    public AudioSource backgroundSource;
    public AudioSource audioSource;
    public Camera AICamera;

    public GameObject door;

    private GameObject player;
    private GameObject imageObject;
    private Image image;
    private static bool tutorialIsFinished;
    private static Vector3 spawnPos;

    GameObject greenCircle;
    GameObject shortwall;
    GameObject tutorialVendor;
    GameObject childPlane;
    GameObject tutorialParent;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        audioSource = VoiceOverObject.GetComponent<AudioSource>();

        Debug.Log("hit");
        greenCircle = GameObject.Find("greencircle");

        shortwall = GameObject.Find("shortwall");
        childPlane = GameObject.Find("childplane");
        tutorialVendor = GameObject.Find("tutorialVendor");
        tutorialParent = GameObject.Find("tutorial");
        

        greenCircle.SetActive(false);
        childPlane.SetActive(false);
        tutorialVendor.SetActive(false);

        if (!tutorialIsFinished)
        {
            audioActivate(0);
        }
        else
        {
            shortwall.SetActive(false);
            door.SetActive(false);
            player.transform.position = new Vector3(spawnPos.x, .25f, spawnPos.z);
            StartCoroutine(sceneManager.GetComponent<SceneScript>().ReskinAllObstacles());
            sceneManager.GetComponent<SceneScript>().TogglePause();
        }
        imageObject = GameObject.Find("GlitchCover");
        image = imageObject.GetComponent<Image>();
    }

    public void audioActivate(int sequenceNum)
    {
        StartCoroutine(playLines(sequenceNum));
    }

    public void SetSpawn(Vector3 pos)
    {
        spawnPos = pos;   
    }

    IEnumerator playLines(int sequenceNum)
    {
        //parent.GetComponent<Baseballs>().isSpeaking = true;
        AudioClip ac;
        float alpha = 0f;
        if (!tutorialIsFinished)
        {
            switch (sequenceNum)
            {
                case 0:
                    ac = tutorialLines[0];
                    audioSource.PlayOneShot(ac); //good morning
                    yield return new WaitForSeconds(tutorialLines[0].length);
                    audioSource.PlayOneShot(tutorialLines[1]); ; //move onto green
                    greenCircle.SetActive(true);
                    break;
                case 1:
                    //controller stops glowing
                    //greenCircle.SetActive(false);
                    audioSource.PlayOneShot(tutorialLines[2]); //here child
                    yield return new WaitForSeconds(2);
                    audioSource.PlayOneShot(tutorialLines[9]); //sparkle
                    childPlane.SetActive(true);
                    yield return new WaitForSeconds(5);
                    audioSource.PlayOneShot(tutorialLines[3]); //bond child
                                                               //animation of child appearing
                    shortwall.transform.position += new Vector3(0, 0, 5);

                    tutorialVendor.SetActive(true);
                    //trigger starts glowing
                    break;
                case 2:
                    audioSource.PlayOneShot(tutorialLines[4]); //child laugh
                    yield return new WaitForSeconds(tutorialLines[4].length);
                    audioSource.PlayOneShot(tutorialLines[5]); //glitch out
                                                               //Fade out animation
                    while (alpha < 1)
                    {
                        yield return new WaitForSeconds(.05f);
                        alpha += .05f;
                        image.color = new Color(0, 0, 0, alpha);
                    }

                    StartCoroutine(sceneManager.GetComponent<SceneScript>().ReskinAllObstacles());

                    yield return new WaitForSeconds(1.5f);
                    yield return new WaitForSeconds(2.5f);
                    audioSource.PlayOneShot(tutorialLines[6]); //DADDY
                    yield return new WaitForSeconds(tutorialLines[6].length);
                    audioSource.PlayOneShot(tutorialLines[7]); //doofenschmirz evil inc. theme song plays
                    yield return new WaitForSeconds(tutorialLines[7].length);
                    //disable tutorial items
                    tutorialVendor.SetActive(false);
                    childPlane.SetActive(false);
                    greenCircle.SetActive(false);
                    GameObject.Find("grass").SetActive(false);
                    GameObject.Find("sun").SetActive(false);
                    //fade back in
                    while (alpha >= 0)
                    {
                        yield return new WaitForSeconds(.05f);
                        alpha -= .05f;
                        image.color = new Color(0, 0, 0, alpha);
                    }
                    audioSource.PlayOneShot(tutorialLines[8]); //go get 'em!
                                                               //glitch animation
                    shortwall.SetActive(false);
                    //door opens
                    //display camera changes to overhead
                    AICamera.GetComponent<AICameraScript>().SwitchCamera();
                    door.GetComponent<MovingDoorScript>().StartMoving();

                    tutorialIsFinished = true;
                    backgroundSource.Play();
                    DontDestroyOnLoad(tutorialParent);

                    break;
                default:
                    break;
            }
        }
    }
}

 



