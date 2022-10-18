using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CellClick : MonoBehaviour
{
    private string parentName;
    private string inputType;
    private string toggleValue;
    private Button button;
    private TMP_Text buttonText;
    private Transform GameArea;
    private Transform child;

    public int house;
    public int index;
    public int column;
    public int row;
    public string currentValue;
    public int[] notes;

    private bool add;

    private bool once = true;

    //public bool notYet = true;

    /*
    void Start()
    {
        GameArea = transform.parent.parent.parent;
        while (notYet)
        {
            notYet = !GameArea.GetComponent<GameArea>().cellStart;
        }
        CellStart();
    }
    */

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnCellClick);
        GameArea = transform.parent.parent.parent;
        child = transform.GetChild(0);
        currentValue = "";
        add = false;
        button = GetComponent<Button>();
        buttonText = child.GetComponent<TMP_Text>();
        parentName = transform.parent.name;
        index = int.Parse(transform.name);
        house = int.Parse(parentName.Substring(parentName.Length - 1));
        notes = new int[9];
        column = GameArea.GetComponent<GameArea>().CalculateColumn(house, index);
        row = GameArea.GetComponent<GameArea>().CalculateRow(house, index);
        //Debug.Log(GameArea.GetComponent<Sudoku>().grid[(row - 1),(column - 1)]);
        if (GameArea.GetComponent<Sudoku>().grid[(row - 1),(column - 1)] != 0)
        {
            string temp = (GameArea.GetComponent<Sudoku>().grid[(row - 1),(column - 1)]).ToString();
            UpdateCell2(temp, true);
        }
    }

    void Update()
    {
        if (GameArea.GetComponent<Sudoku>().grid[(row - 1),(column - 1)] != 0)
        {
            if (once)
            {
                once = false;
                string temp = (GameArea.GetComponent<Sudoku>().grid[(row - 1),(column - 1)]).ToString();
                UpdateCell2(temp, true);
            }
        }
    }

    public void OnCellClick()
	{
        inputType = GameArea.GetComponent<GameArea>().inputType;
        toggleValue = GameArea.GetComponent<GameArea>().toggleValue;

        if (inputType == "numbers")
        {
            // Number
            if (toggleValue == "C")
            {
                if (currentValue != "")
                {
                    add = false;
                    UpdateCell("", add);
                }
            }
            else if (toggleValue == "")
            {
                // Do Nothing
            }
            else
            {
                if (currentValue != toggleValue)
                {
                    add = true;
                    UpdateCell(toggleValue, add);
                }
            }
        }
        else
        {
            // Notes
        }

    }

    void UpdateCell(string input, bool add)
    {
        if (!add)
        {
            buttonText.text = input;
            GameArea.GetComponent<GameArea>().UpdateCellValue(column, row, house, int.Parse(currentValue), add);
            currentValue = "";
        }
        else if (add != GameArea.GetComponent<GameArea>().CheckColumnValue(column,int.Parse(toggleValue)))
        {
            if (add != GameArea.GetComponent<GameArea>().CheckRowValue(row,int.Parse(toggleValue)))
            {
                if (add != GameArea.GetComponent<GameArea>().CheckHouseValue(house,int.Parse(toggleValue)))
                {
                    // Valid placement
                    buttonText.text = input;
                    currentValue = toggleValue;
                    GameArea.GetComponent<GameArea>().UpdateCellValue(column, row, house, int.Parse(toggleValue), add);
                }
                else
                {
                    // Invalid House
                    //Debug.Log("Invalid House");
                }
            }
            else
            {
                // Invalid Row
                //Debug.Log("Invalid Row");
            }
        }
        else
        {
            // Invalid Column
            //Debug.Log("Invalid Column");
        }

        add = false;
    }

    void UpdateCell2(string input, bool add)
    {
        buttonText.text = input;
        currentValue = input;
        GameArea.GetComponent<GameArea>().UpdateCellValue(column, row, house, int.Parse(currentValue), add);
        add = false;
    }
    
}
