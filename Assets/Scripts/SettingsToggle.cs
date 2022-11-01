using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsToggle : MonoBehaviour
{
    private bool isToggled;
    private Image image;
    private Button button;

    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnToggleClick);
        if ((PlayerPrefs.GetInt("ScrollingBackgrounds", 1) == 1) && (transform.name == "ScrollingBackgroundsToggle"))
        {
            isToggled = true;
            image.color = Color.green;
        }
        else if ((PlayerPrefs.GetInt("ScrollingBackgrounds", 1) == 0) && (transform.name == "ScrollingBackgroundsToggle"))
        {
            isToggled = false;
            image.color = Color.white;
        }
        if ((PlayerPrefs.GetInt("Audio", 1) == 1) && (transform.name == "AudioToggle"))
        {
            isToggled = true;
            image.color = Color.green;
        }
        else if ((PlayerPrefs.GetInt("Audio", 1) == 0) && (transform.name == "AudioToggle"))
        {
            isToggled = false;
            image.color = Color.white;
        }
    }

    private void OnToggleClick()
    {
        if (isToggled)
        {
            Toggle(false);
            if (transform.name == "ScrollingBackgroundsToggle")
            {
                PlayerPrefs.SetInt("ScrollingBackgrounds", 0);
            }
            else if (transform.name == "AudioToggle")
            {
                PlayerPrefs.SetInt("Audio", 0);
            }
            
        }
        else
        {
            Toggle(true);
            if (transform.name == "ScrollingBackgroundsToggle")
            {
                PlayerPrefs.SetInt("ScrollingBackgrounds", 1);
            }
            else if (transform.name == "AudioToggle")
            {
                PlayerPrefs.SetInt("Audio", 1);
            }
        }
        PlayerPrefs.Save();
    }

    public void Toggle(bool toggle)
    {
        if (toggle && !isToggled)
        {
            isToggled = !isToggled;
            image.color = Color.green;
        }
        else if (!toggle && isToggled)
        {
            isToggled = !isToggled;
            image.color = Color.white;
        }
    }
}