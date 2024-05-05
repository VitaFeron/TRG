using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class levelTranscition : MonoBehaviour
{
    public string scene;
    public GameObject fadeIn;
    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }
    
    public void Finish()
    {
        fadeIn.SetActive(true);
    }
}