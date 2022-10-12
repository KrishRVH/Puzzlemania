using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleObject : MonoBehaviour
{

    public Color defaultColor;
    public Color toggleColor;
    private bool toggled;
    private Toggle toggle;
    private Image image;

    public void Toggle()
    {
        toggled = toggle.isOn;
        if (toggled)
        {
            image.color = toggleColor;
        }
        else
        {
            image.color = defaultColor;
        }
    }

    void Start()
    {
        image = GetComponent<Image>();
        toggle = GetComponent<Toggle>();
        toggled = toggle.isOn;
        if (toggled)
        {
            image.color = toggleColor;
        }
        else
        {
            image.color = defaultColor;
        }
    }
}
