using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetCard : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;
    private Image image;
    private Transform gameArea;
    private Color defaultColor;
    private Color selectedColor;
    public bool selected;

    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(CardClick);
        image = GetComponent<Image>();
        gameArea = transform.parent;
        defaultColor = Color.white;
        selectedColor = new Color(1f, 1f, 1f, 0.7f);
        selected = false;
    }

    private void CardClick()
    {
        if (selected)
        {
            selected = false;
            image.color = defaultColor;
        }
        else
        {
            selected = true;
            image.color = selectedColor;
        }
    }
}