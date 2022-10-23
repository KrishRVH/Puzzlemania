using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuNote : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;
    private string noteValue;
    private Transform gameArea;
    private Transform cell;
    private Transform notes;
    private Transform toggles;
    private Transform inputTypes;

    void Start()
    {
        noteValue = transform.name;
        gameArea = transform.parent.parent.parent.parent;
        notes = transform.parent;
        cell = transform.parent.parent;
        toggles = gameArea.GetChild(10);
        inputTypes = gameArea.GetChild(11);
        button = GetComponent<Button>();
        button.onClick.AddListener(OnNoteClick);
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void OnNoteClick()
    {
        string currentDigit = GetCurrentDigitToggle();
        if (GetCurrentInputType())
        {
            if (currentDigit == noteValue)
            {
                if (buttonText.text != noteValue)
                {
                    EnableNote(true);
                }
                else
                {
                    EnableNote(false);
                }
            }
            else
            {
                notes.GetChild(int.Parse(currentDigit) - 1).GetComponent<SudokuNote>().OnNoteClick();
            }
        }
        else
        {
            notes.GetComponent<SudokuNotes>().EnableNotes(false);
            cell.GetComponent<SudokuCell>().OnCellClick();
        }
    }

    private string GetCurrentDigitToggle()
    {
        Transform temp;
        for (int i = 0; i < 10; i++)
        {
            temp = toggles.GetChild(i);
            if (temp.GetComponent<SudokuToggleDigit>().IsToggled())
            {
                return temp.name;
            }
        }
        return "";
    }

    public string GetCurrentValue()
    {
        return buttonText.text;
    }

    public void EnableNote(bool enable)
    {
        if (enable)
        {
            buttonText.text = noteValue;
        }
        else
        {
            buttonText.text = "";
        }
    }

    public bool GetCurrentInputType()
    {
        Transform temp;
        for (int i = 0; i < 2; i++)
        {
            temp = inputTypes.GetChild(i);
            if (temp.GetComponent<SudokuToggleInputType>().IsToggled())
            {
                if (temp.name == "Number")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        return false;
    }
}