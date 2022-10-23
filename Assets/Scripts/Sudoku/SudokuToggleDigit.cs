using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuToggleDigit : MonoBehaviour
{
    private Color notToggled = new Color(1f,1f,1f,1f);
    private Color toggled = new Color(0f,1f,0f,1f);
    private bool isToggled;
    private Image image;
    private Button button;

    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnToggleClick);
        if (transform.name == "C")
        {
            isToggled = true;
            image.color = toggled;
        }
        else
        {
            isToggled = false;
            image.color = notToggled;
        }
    }

    public bool IsToggled()
    {
        return isToggled;
    }

    private void OnToggleClick()
    {
        if (isToggled)
        {
            return;
        }
        else
        {
            UnToggleCheck();
            Toggle(true);
        }
    }

    private void UnToggleCheck()
    {
        List<string> list = new List<string>();
        string[] stringArray = {"1","2","3","4","5","6","7","8","9","C"};
        list.AddRange(stringArray);

        for (int i = 0; i < 10; i++)
        {
            if (list[i] == transform.name)
            {
                continue;
            }
            Transform temp = transform.parent.GetChild(i);
            if (temp.GetComponent<SudokuToggleDigit>().IsToggled())
            {
                temp.GetComponent<SudokuToggleDigit>().Toggle(false);
            }
        }
    }

    public void Toggle(bool toggle)
    {
        if (toggle && isToggled)
        {
            return;
        }
        else if (toggle && !isToggled)
        {
            isToggled = !isToggled;
            image.color = toggled;
        }
        else if (!toggle && isToggled)
        {
            isToggled = !isToggled;
            image.color = notToggled;
        }
        else if (!toggle && !isToggled)
        {
            return;
        }
    }
}