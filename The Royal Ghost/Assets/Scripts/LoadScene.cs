using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    int levelUnlock;
    public Button[] buttons;
    public GameObject fadeIn;
    //public GameObject fadeOut;
    // Start is called before the first frame update
    void Start()
    {
        fadeIn.SetActive(false);
        levelUnlock = PlayerPrefs.GetInt("levels", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        if (buttons != null && buttons.Length > 0)
        {
            for (int i = 0; i < Mathf.Min(levelUnlock, buttons.Length); i++)
            {
                buttons[i].interactable = true;
            }
        }
    }

    public void NextLevel(string levelName)
    {
        Time.timeScale = 1;
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
}
