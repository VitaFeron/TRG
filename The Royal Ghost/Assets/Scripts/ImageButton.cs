using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageButton : MonoBehaviour
{
    public Image imageButton;
    public Vector3 originalScale;
    public Vector3 enlargedScale;
    public RectTransform buttonRect;
    public Button button;

    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        {
            if (button.interactable)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(buttonRect, mousePosition))
                {
                    OnPointerEnter();
                }
                else
                {
                    OnPointerExit();
                }
            }
        }
    }

    public void OnPointerEnter()
    {
        imageButton.transform.localScale = enlargedScale;
    }

    public void OnPointerExit()
    {
        imageButton.transform.localScale = originalScale;
    }
}
