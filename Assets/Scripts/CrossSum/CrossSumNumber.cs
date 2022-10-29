using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumNumber : MonoBehaviour
{
    public int index;
    private Button button;
    private TMP_Text buttonText;
    private Transform toggles;
    private Transform gameArea;
    private Transform numbers;
    private Transform notes;
    private Transform inputTypes;
    private Image image;
    private Transform leftPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnNumberClick);
        buttonText = transform.GetChild(1).GetComponent<TMP_Text>();
        image = GetComponent<Image>();
        gameArea = transform.parent.parent;
        numbers = transform.parent;
        toggles = gameArea.GetChild(5);
        inputTypes = gameArea.GetChild(6);
        index = int.Parse(transform.name);
        notes = transform.GetChild(0);
        leftPanel = GameObject.Find("LeftPanel").transform;
        if (buttonText.text == "")
        {
            EnableNotes(true);
        }
        else
        {
            EnableNotes(false);
        }
    }

    public void OnNumberClick()
    {
        string currentDigit = GetCurrentDigitToggle();
        if (GetCurrentInputType())
        {
            if (currentDigit == "C")
            {
                if (buttonText.text == "")
                {
                    notes.GetComponent<CrossSumNotes>().ClearNotes();
                }
                else
                {
                    TrackChange();
                    EnableNotes(true);
                    buttonText.text = "";
                }

            }
            else if (currentDigit == buttonText.text)
            {
                TrackChange();
                EnableNotes(true);
                buttonText.text = "";
            }
            else
            {
                if (IsValidPlacement(currentDigit))
                {
                    TrackChange();
                    EnableNotes(false);
                    buttonText.text = currentDigit;
                }
            }
        }
        else
        {
            if (buttonText.text == "")
            {
                if (currentDigit == "C")
                {
                    notes.GetComponent<CrossSumNotes>().ClearNotes();
                }
                else
                {
                    notes.GetComponent<CrossSumNotes>().EnableNote(int.Parse(currentDigit));
                }
            }
        }
        IsPuzzleFinished();
    }

    private void IsPuzzleFinished()
    {
        for (int i = 0; i < 9; i++)
        {
            if (numbers.GetChild(i).GetComponent<CrossSumNumber>().GetCurrentValue() == "")
            {
                return;
            }
        }
        gameArea.GetComponent<CrossSumPuzzle>().CheckSolution();
    }

    public string GetCurrentValue()
    {
        return buttonText.text;
    }

    private string GetCurrentDigitToggle()
    {
        for (int i = 0; i < 10; i++)
        {
            Transform temp = toggles.GetChild(i);
            if (temp.GetComponent<CrossSumToggleDigit>().IsToggled())
            {
                return temp.name;
            }
        }
        return "";
    }

    public bool GetCurrentInputType()
    {
        for (int i = 0; i < 2; i++)
        {
            Transform temp = inputTypes.GetChild(i);
            if (temp.GetComponent<CrossSumToggleInputType>().IsToggled())
            {
                if (temp.name == "Number")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        return false;
    }

    bool IsValidPlacement(string value)
    {
        for (int i = 0; i < 9; i++)
        {
            if (value == numbers.GetChild(i).GetComponent<CrossSumNumber>().GetCurrentValue())
            {
                return false;
            }
        }
        return true;
    }

    public void EnableNotes(bool enable)
    {
        notes.GetComponent<CrossSumNotes>().EnableNotes(enable);
    }

    private void TrackChange()
    {
        Transform undoButton = leftPanel.GetChild(0).GetComponent<Transform>();
        undoButton.GetComponent<CrossSumUndo>().AddNumberChange(index, buttonText.text);
    }

    public void UndoChange(string savedValue)
    {
        buttonText.text = savedValue;
        if (buttonText.text == "")
        {
            EnableNotes(true);
        }
        else
        {
            EnableNotes(false);
        }
    }
}