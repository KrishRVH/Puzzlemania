using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuCell : MonoBehaviour
{
    private int row;
    private int column;
    private int house;
    private int index;
    private Button button;
    private TMP_Text buttonText;
    private bool canChange = false;
    private Transform toggles;
    private Transform gameArea;
    private Transform notes;
    private Transform inputTypes;
    private Image image;
    private Color canChangeFalse;
    private Color canChangeTrue;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCellClick);
        buttonText = transform.GetChild(1).GetComponent<TMP_Text>();
        image = GetComponent<Image>();
        canChangeFalse = new Color(0.7f,0.7f,0.7f,1f);
        canChangeTrue = new Color(1f,1f,1f,1f);
        gameArea = transform.parent.parent;
        toggles = gameArea.GetChild(10);
        inputTypes = gameArea.GetChild(11);
        house = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 1));
        index = int.Parse(transform.name);
        row = CalculateRow(house, index);
        column = CalculateColumn(house, index);
        notes = transform.GetChild(0);
        GetValue();
        if (buttonText.text == "")
        {
            EnableNotes(true);
        }
        else
        {
            EnableNotes(false);
        }
    }

    public void OnCellClick()
    {
        string currentDigit = GetCurrentDigitToggle();
        if (GetCurrentInputType())
        {
            if (canChange)
            {
                if (currentDigit == "C")
                {
                    if (buttonText.text == "")
                    {
                        notes.GetComponent<SudokuNotes>().ClearNotes();
                    }
                    else
                    {
                        EnableNotes(true);
                        buttonText.text = "";
                    }

                }
                else if (currentDigit == buttonText.text)
                {
                    EnableNotes(true);
                    buttonText.text = "";
                }
                else
                {
                    EnableNotes(false);
                    buttonText.text = currentDigit;
                }
            }
        }
        else
        {
            if (buttonText.text == "")
            {
                notes.GetComponent<SudokuNotes>().EnableNote(int.Parse(currentDigit));
            }
        }
        IsPuzzleFinished();
    }

    private void IsPuzzleFinished()
    {
        Transform houseTemp;
        for (int i = 0; i < 9; i++)
        {
            houseTemp = gameArea.GetChild(i + 1);
            for (int j = 0; j < 9; j++)
            {
                if (houseTemp.GetChild(j).GetComponent<SudokuCell>().GetCurrentValue() == "")
                {
                    return;
                }
            }
        }
        gameArea.GetComponent<SudokuPuzzle>().CheckSolution();
    }

    private void GetValue()
    {
        int cellValue = gameArea.GetComponent<SudokuPuzzle>().sudokuPuzzle.GetCellValue(row, column);
        if (cellValue != 0)
        {
            buttonText.text = cellValue.ToString();
            image.color = canChangeFalse;
        }
        else
        {
            image.color = canChangeTrue;
            canChange = true;
        }
    }

    public string GetCurrentValue()
    {
        return buttonText.text;
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

    private int CalculateRow(int house, int index)
    {
        switch(((house / 3) * 10) + (index / 3))
        {
            case 00: return 0;
            case 01: return 1;
            case 02: return 2;
            case 10: return 3;
            case 11: return 4;
            case 12: return 5;
            case 20: return 6;
            case 21: return 7;
            case 22: return 8;
            default: return 0;
        }
    }

    private int CalculateColumn(int house, int index)
    {
        switch(((house % 3) * 10) + (index % 3)) 
        {
            case 00: return 0;
            case 01: return 1;
            case 02: return 2;
            case 10: return 3;
            case 11: return 4;
            case 12: return 5;
            case 20: return 6;
            case 21: return 7;
            case 22: return 8;
            default: return 0;
        }
    }

    public void EnableNotes(bool enable)
    {
        notes.GetComponent<SudokuNotes>().EnableNotes(enable);
    }
}