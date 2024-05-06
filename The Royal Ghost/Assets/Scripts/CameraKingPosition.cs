using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraKingPosition : MonoBehaviour  //скрипт должен висеть на пустом объекте с коллайдером
{
    private Camera cameraDefault;
    public Vector3 cameraPosition;
    private GameObject king;
    public Vector3 kingPosition;

    private void Start()
    {
        cameraDefault = Camera.main;
        king = GameObject.FindGameObjectWithTag("King");
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("King") || collider.CompareTag("Ghost"))
        {
            cameraDefault.transform.position = cameraPosition;
            king.transform.position = kingPosition;
        }
    }
}
