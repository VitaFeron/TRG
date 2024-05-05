using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAll : MonoBehaviour
{
    public GameObject fadeIn;

    private void Start()
    {
        fadeIn.SetActive(false);
    }
}
