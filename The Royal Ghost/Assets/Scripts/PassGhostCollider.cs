using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassGhostCollider : MonoBehaviour
{
    public GameObject[] platforms; 
    public GameObject ghost;
    public GameObject king;
    void Update()
    {
        if (king.activeInHierarchy)
        {
            foreach (GameObject platform in platforms)
            {
                platform.GetComponent<BoxCollider2D>().isTrigger = false;
            }
        }
        else if (ghost.activeInHierarchy)
        {
            foreach (GameObject platform in platforms)
            {
                platform.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }         
    }
}
