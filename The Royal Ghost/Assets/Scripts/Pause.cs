using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject cutsceneObject;
    public GameObject resumeButton;
    public GameObject nextLevelButton;
    public GameObject pauseText;
    private GameObject winText;
    public GameObject fadeIn;
    private GameObject king;
    private bool doorColliderBool;
    private int isPaused;
    private King kingScript;
    private Animator anim;
    private Rigidbody2D rb;

    //private Vector3 kingPosition;

    void Start()
    {
        king = GameObject.FindGameObjectWithTag("King");
        fadeIn.SetActive(false);
        kingScript = king.GetComponent<King>();
        anim = king.GetComponent<Animator>();
        rb = king.GetComponent<Rigidbody2D>();

        cutsceneObject = GameObject.FindGameObjectWithTag("Table");
        resumeButton = GameObject.FindGameObjectWithTag("Resume");
        nextLevelButton = GameObject.FindGameObjectWithTag("NextLevel");
        pauseText = GameObject.FindGameObjectWithTag("PauseText");
        winText = GameObject.FindGameObjectWithTag("WinText");
        cutsceneObject.SetActive(false);
        resumeButton.SetActive(false);
        nextLevelButton.SetActive(false);
        pauseText.SetActive(false);
        winText.SetActive(false);
    }

    void Update()
    {
        isPaused = king.GetComponent<King>().isPaused;
        doorColliderBool = king.GetComponent<King>().doorColliderBool;

        if (Input.GetKeyDown(KeyCode.Escape) && doorColliderBool == false && isPaused == 0)
        {
            resumeButton.SetActive(true);
            nextLevelButton.SetActive(false);
            pauseText.SetActive(true);
            winText.SetActive(false);
            //cutsceneObject.SetActive(true);
            //Invoke("Stop", 1.5f);
            StartCoroutine(kingStop(0));
                     
        }

        if (doorColliderBool == true && isPaused == 1)
        {
            resumeButton.SetActive(false);
            nextLevelButton.SetActive(true);
            pauseText.SetActive(false);
            winText.SetActive(true);
            //cutsceneObject.SetActive(true);
            //Invoke("Stop", 1.5f);
            StartCoroutine(kingStop1(0));                       
        }
    }
    IEnumerator Table(float time)
    {
        yield return new WaitForSeconds(time);
        //Time.timeScale = 0;
        //isPaused = 1;
        
    }
    IEnumerator Table1(float time)
    {
        yield return new WaitForSeconds(time);
        //Time.timeScale = 0;
        //isPaused = 0;
    }
    IEnumerator kingStop(float time)
    {
        yield return new WaitForSeconds(time);
        kingScript.enabled = false;
        anim.speed = 0.0f;
        rb.Sleep();
        cutsceneObject.SetActive(true);
        StartCoroutine(Table(1.5f));
    }
    IEnumerator kingStop1(float time)
    {
        yield return new WaitForSeconds(time);
        kingScript.enabled = false;
        anim.speed = 0.0f;
        rb.Sleep();
        cutsceneObject.SetActive(true);
        StartCoroutine(Table1(1.5f));
    }

    public void Resume()
    {
        //Time.timeScale = 1;
        isPaused = 0;
        kingScript.enabled = true;
        anim.speed = 1.0f;
        rb.WakeUp();
        cutsceneObject.SetActive(false);
    }

    public void Levels()
    {
        Time.timeScale = 1;
        fadeIn.SetActive(true);
        StartCoroutine(Transition("Levels"));       
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(FindButtonsAfterRestart());
    }

    public void NextLevel(string levelName)
    {
        Time.timeScale = 1;
        isPaused = 0;
        fadeIn.SetActive(true);
        //Debug.Log("Активировал");
        StartCoroutine(Transition(levelName));
    }

    IEnumerator Transition(string levelName)
    {
        yield return new WaitForSeconds(1f);
        //Debug.Log("Дождался");
        SceneManager.LoadScene(levelName);
    }

    IEnumerator FindButtonsAfterRestart()
    {
        yield return new WaitForSeconds(0.1f); // Небольшая задержка для загрузки сцены

        Time.timeScale = 1;
        cutsceneObject = GameObject.FindGameObjectWithTag("Table");
        resumeButton = GameObject.FindGameObjectWithTag("Resume");
        nextLevelButton = GameObject.FindGameObjectWithTag("NextLevel");
        pauseText = GameObject.FindGameObjectWithTag("PauseText");
        winText = GameObject.FindGameObjectWithTag("WinText");

        cutsceneObject.SetActive(false);
    }
}
