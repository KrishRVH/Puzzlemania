using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDigit : MonoBehaviour
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
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            Toggle();
        });
        image = GetComponent<Image>();
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
