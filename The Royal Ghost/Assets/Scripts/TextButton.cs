using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextButton : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public float originalFontSize;
    public float enlargedFontSize;
    public RectTransform buttonRect;
    public Button button;
    public Image padblock;
    public TextMeshProUGUI textButtonOpen;

    private void Update()
    {
        Vector2 mousePosition= Input.mousePosition;
        {
            if (button.interactable)
            {
                textButtonOpen.enabled = true;
                padblock.enabled = false;
                if (RectTransformUtility.RectangleContainsScreenPoint(buttonRect, mousePosition))
                {
                    OnPointerEnter();
                }
                else
                {
                    OnPointerExit();
                }
            }
            else
            {
                textButtonOpen.enabled = false;
                padblock.enabled = true;
            }
        }
    }

    public void OnPointerEnter()
    {
        textMeshPro.fontSize = enlargedFontSize;
    }

    public void OnPointerExit()
    {
        textMeshPro.fontSize = originalFontSize;
    }
}

