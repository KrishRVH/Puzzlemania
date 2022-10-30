using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuCell : MonoBehaviour
{
    private int row;
    private int column;
    public int house;
    public int index;
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
    private Transform leftPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCellClick);
        buttonText = transform.GetChild(1).GetComponent<TMP_Text>();
        image = GetComponent<Image>();
        canChangeFalse = new Color(0.7f,0.7f,0.7f,1f);
        canChangeTrue = new Color(1f,1f,1f,1f);
        gameArea = transform.parent.parent;
        toggles = gameArea.GetChild(9);
        inputTypes = gameArea.GetChild(10);
        house = int.Parse(transform.parent.name.Substring(transform.parent.name.Length - 1));
        index = int.Parse(transform.name);
        row = CalculateRow(house, index);
        column = CalculateColumn(house, index);
        notes = transform.GetChild(0);
        leftPanel = GameObject.Find("LeftPanel").transform;
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
                        TrackChange();
                        EnableNotes(true);
                        UpdateDigitCount(buttonText.text, false);
                        buttonText.text = "";
                    }

                }
                else if (currentDigit == buttonText.text)
                {
                    TrackChange();
                    EnableNotes(true);
                    UpdateDigitCount(currentDigit, false);
                    buttonText.text = "";
                }
                else
                {
                    if (IsValidPlacement(currentDigit))
                    {
                        TrackChange();
                        EnableNotes(false);
                        UpdateDigitCount(currentDigit, true);
                        buttonText.text = currentDigit;
                    }
                }
            }
        }
        else
        {
            if (buttonText.text == "")
            {
                if (currentDigit == "C")
                {
                    notes.GetComponent<SudokuNotes>().ClearNotes();
                }
                else
                {
                    notes.GetComponent<SudokuNotes>().EnableNote(int.Parse(currentDigit));
                }
            }
        }
        IsPuzzleFinished();
    }

    private void IsPuzzleFinished()
    {
        Transform houseTemp;
        for (int i = 0; i < 9; i++)
        {
            houseTemp = gameArea.GetChild(i);
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

    IEnumerator ConflictAnimation()
    { 
        image.color = Color.red;
        yield return new WaitForSeconds(0.35f);
        if (canChange)
        {
            image.color = canChangeTrue;
        }
        else
        {
            image.color = canChangeFalse;
        }
    }

    public void ShowConflict()
    { 
        StartCoroutine(ConflictAnimation());
    }

    private void GetValue()
    {
        int cellValue = gameArea.GetComponent<SudokuPuzzle>().GetCellValue(row, column);
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

    private void UpdateDigitCount(string value, bool add)
    {
        gameArea.GetComponent<SudokuPuzzle>().UpdateDigitCount((int.Parse(value) - 1), add);
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

    bool IsValidPlacement(string value)
    {
        bool output = true;
        if (!IsValidRowPlacement(value)) { output = false;}
        if (!IsValidColumnPlacement(value)) { output = false; }
        if (!IsValidHousePlacement(value)) { output = false; }
        return output;
    }

    bool IsValidRowPlacement(string value)
    {
        List<int> houseList = new List<int>();
        List<int> cellList = new List<int>();

        houseList.Add(house - (house % 3));
        houseList.Add((house - (house % 3)) + 1);
        houseList.Add((house - (house % 3)) + 2);

        cellList.Add(index - (index % 3));
        cellList.Add((index - (index % 3)) + 1);
        cellList.Add((index - (index % 3)) + 2);

        foreach (int houseNum in houseList)
        {
            if (houseNum == house) { continue; }

            Transform houseTemp = gameArea.GetChild(houseNum);
            foreach (int indexNum in cellList)
            {
                if (value == houseTemp.GetChild(indexNum).GetComponent<SudokuCell>().GetCurrentValue())
                {
                    houseTemp.GetChild(indexNum).GetComponent<SudokuCell>().ShowConflict();
                    return false;
                }
            }
        }
        return true;
    }

    bool IsValidColumnPlacement(string value)
    {
        List<int> houseList = new List<int>();
        List<int> cellList = new List<int>();

        houseList.Add((house % 3));
        houseList.Add(((house % 3)) + 3);
        houseList.Add(((house % 3)) + 6);

        cellList.Add((index % 3));
        cellList.Add(((index % 3)) + 3);
        cellList.Add(((index % 3)) + 6);

        foreach (int houseNum in houseList)
        {
            if (houseNum == house) { continue; }

            Transform houseTemp = gameArea.GetChild(houseNum);
            foreach (int indexNum in cellList)
            {
                if (value == houseTemp.GetChild(indexNum).GetComponent<SudokuCell>().GetCurrentValue())
                {
                    houseTemp.GetChild(indexNum).GetComponent<SudokuCell>().ShowConflict();
                    return false;
                }
            }
        }
        return true;
    }

    bool IsValidHousePlacement(string value)
    {
        Transform houseTemp = transform.parent;
        for (int i = 0; i < 9; i++)
        {
            if (value == houseTemp.GetChild(i).GetComponent<SudokuCell>().GetCurrentValue())
            {
                houseTemp.GetChild(i).GetComponent<SudokuCell>().ShowConflict();
                return false;
            }
        }
        return true;
    }

    public void EnableNotes(bool enable)
    {
        notes.GetComponent<SudokuNotes>().EnableNotes(enable);
    }

    private void TrackChange()
    {
        Transform undoButton = leftPanel.GetChild(0).GetComponent<Transform>();
        undoButton.GetComponent<SudokuUndo>().AddNumberChange(house, index, buttonText.text);
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