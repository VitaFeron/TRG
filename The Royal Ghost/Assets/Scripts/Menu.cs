using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject options;
    public GameObject fadeIn;

    public void Play()
    {
        fadeIn.SetActive(true);
        StartCoroutine(Trans(1.5f));
    }

    IEnumerator Trans(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Levels");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Options()
    {
        menu.SetActive(false);
        options.SetActive(true);
    }

    public void Back()
    {
        menu.SetActive(true);
        options.SetActive(false);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
