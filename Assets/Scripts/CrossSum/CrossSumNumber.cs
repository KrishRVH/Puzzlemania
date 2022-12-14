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
    private Transform results;
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
        results = gameArea.GetChild(4);
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
                    DisableToggle(buttonText.text, false);
                    buttonText.text = "";
                }

            }
            else if (currentDigit == buttonText.text)
            {
                TrackChange();
                EnableNotes(true);
                DisableToggle(currentDigit, false);
                buttonText.text = "";
            }
            else
            {
                if (IsValidPlacement(currentDigit))
                {
                    RemoveConflictingNotes(currentDigit);
                    TrackChange();
                    EnableNotes(false);
                    if (buttonText.text != "")
                    {
                        DisableToggle(buttonText.text, false);
                    }
                    DisableToggle(currentDigit, true);
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
        IsRowFinished();
        IsColumnFinished();
        IsPuzzleFinished();
    }

    private void DisableToggle(string value, bool disable)
    {
        int index = (int.Parse(value) - 1);
        if (disable)
        {
            toggles.GetChild(index).GetComponent<CrossSumToggleDigit>().DisableToggle(disable);
            for (int i = (index + 1); i < 11; i++)
            {
                if (toggles.GetChild(i).GetComponent<CrossSumToggleDigit>().IsDisabled())
                {
                    continue;
                }
                toggles.GetChild(i).GetComponent<CrossSumToggleDigit>().Toggle(true);
                break;
            }
        }
        else
        {
            toggles.GetChild(index).GetComponent<CrossSumToggleDigit>().DisableToggle(disable);
        }

    }

    private void IsRowFinished()
    {
        for (int i = ((index / 3) * 3); i < (((index / 3) * 3) + 3); i++)
        {
            if (numbers.GetChild(i).GetComponent<CrossSumNumber>().GetCurrentValue() == "")
            {
                results.GetChild((index / 3)).GetComponent<CrossSumResult>().ResetColor();
                return;
            }
        }
        gameArea.GetComponent<CrossSumPuzzle>().ValidateRowResult(((index / 3) * 3));
    }

    private void IsColumnFinished()
    {
        for (int i = (index % 3); i <= ((index % 3) + 6); i += 3)
        {
            if (numbers.GetChild(i).GetComponent<CrossSumNumber>().GetCurrentValue() == "")
            {
                results.GetChild(((index % 3) + 3)).GetComponent<CrossSumResult>().ResetColor();
                return;
            }
        }
        gameArea.GetComponent<CrossSumPuzzle>().ValidateColumnnResult((index % 3));
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
        if (savedValue == "")
        {
            DisableToggle(buttonText.text, false); 
        }
        else
        {
            DisableToggle(savedValue, true);
        }
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

    private void RemoveConflictingNotes(string value)
    {
        for (int i = 0; i < 9; i++)
        {
            if (value == transform.parent.GetChild(i).GetChild(0).GetChild((int.Parse(value) - 1)).GetComponent<CrossSumNote>().GetCurrentValue())
            {
                transform.parent.GetChild(i).GetChild(0).GetChild((int.Parse(value) - 1)).GetComponent<CrossSumNote>().EnableNote(false);
            }
        }
    }
}