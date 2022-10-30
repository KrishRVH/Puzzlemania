using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuToggleDigit : MonoBehaviour
{
    private bool isToggled;
    private Image image;
    private Button button;
    private bool isDisabled = false;

    void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnToggleClick);
        if (transform.name == "C")
        {
            isToggled = true;
            image.color = Color.green;
        }
        else
        {
            isToggled = false;
            image.color = Color.white;
        }
    }

    public bool IsToggled()
    {
        return isToggled;
    }

    private void OnToggleClick()
    {
        if (!isDisabled)
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
    }

    public bool IsDisabled()
    {
        return isDisabled;
    }

    public void DisableToggle(bool disable)
    {
        isDisabled = disable;
        if (isDisabled)
        {
            isToggled = false;
            image.color = Color.gray;
        }
        else if (isToggled)
        {
            image.color = Color.green;
        }
        else
        {
            image.color = Color.white;
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
            image.color = Color.green;;
        }
        else if (!toggle && isToggled)
        {
            isToggled = !isToggled;
            image.color = Color.white;;
        }
        else if (!toggle && !isToggled)
        {
            return;
        }
    }
}