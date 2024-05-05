using UnityEngine;

public class TransparentBlock : MonoBehaviour
{
    private SpriteRenderer objectRenderer;
    private Collider2D objectCollider;
    private bool isTransparent = false;

    void Start()
    {
        objectRenderer = GetComponent<SpriteRenderer>();
        objectCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isTransparent && objectRenderer.material.color.a > 0)
        {
            Color currentColor = objectRenderer.material.color;
            currentColor.a -= Time.deltaTime; // Уменьшаем альфа-канал для плавного исчезновения
            objectRenderer.material.color = currentColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("King"))
        {
            isTransparent = true;
            objectCollider.isTrigger = true; // Делаем коллайдер неактивным
        }
    }
}