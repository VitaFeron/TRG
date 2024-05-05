using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyPlatforms : MonoBehaviour
{
    public GameObject[] destroyPlatforms;
    public GameObject destroyPlatform;
    public GameObject DestroyEffect;
    public float fadeSpeed = 2f;
    private Collider2D colliderDestroyPlatform;
    private bool isFading = false;

    private void Start()
    {
        colliderDestroyPlatform = destroyPlatform.GetComponent<BoxCollider2D>();
    }

    IEnumerator FadePlatformNot(float speedNot, GameObject platform, Color originalColor)
    {
        yield return new WaitForSeconds(speedNot);
        platform.SetActive(true);
        colliderDestroyPlatform.enabled = true;
        SpriteRenderer spriteRenderer = platform.GetComponent<SpriteRenderer>();
        spriteRenderer.material.color = originalColor;
    }

    IEnumerator FadePlatform(GameObject platform)
    {
        SpriteRenderer spriteRenderer = platform.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.material.color;
        Color color = originalColor;

        while (color.a > 0)
        {
            color.a -= fadeSpeed * Time.deltaTime;
            spriteRenderer.material.color = color;

            yield return null;
        }
        Instantiate(DestroyEffect, platform.transform.position, Quaternion.identity);
        platform.SetActive(false);
        colliderDestroyPlatform.enabled = false;
        isFading = false;
        StartCoroutine(FadePlatformNot(10f, platform, originalColor));
    }

    // ... (остальной код) ...
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("King") && isFading == false)
        {
            isFading = true;
            foreach (GameObject destroyPlatform in destroyPlatforms)
            {
                StartCoroutine(FadePlatform(destroyPlatform));
            }
        }
    }

}
