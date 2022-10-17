using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour
{
    private int[,] grid;
    private bool[,] cList;
    private bool[,] rList;
    private bool[,] hList;
    private Button hintButton;
    private bool reset = false;
    private int resetCount = 0;

    void Start()
    {
        hintButton = GetComponent<Button>();
        hintButton.onClick.AddListener(HintClick);
        grid = new int[9,9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                grid[i,j] = 0;
            }
        }
        //Debug.Log ("0 Grid");
        //PrintGrid();
        cList = new bool[9,9];
        rList = new bool[9,9];
        hList = new bool[9,9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                cList[i,j] = true;
                rList[i,j] = true;
                hList[i,j] = true;
            }
        }
        //FillGrid();
    }

    void HintClick()
    {
        FillGrid();
        PrintGrid();
        if (reset) {
            resetCount++;
            reset = false;
            if (resetCount < 20)
            {
                HintClick();
            }
            else
            {
                Debug.Log("Reset Limit Reached");
            }
        }
    }

    bool CheckRowForValue(int row, int value)
    {
        int gridValue;
        for (int i = 0; i < 9; i++)
        {
            gridValue = grid[row,i];
            if (gridValue == value)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckColumnForValue(int column, int value)
    {
        int gridValue;
        for (int i = 0; i < 9; i++)
        {
            gridValue = grid[i,column];
            if (gridValue == value)
            {
                return true;
            }
        }
        return false;
    }

    bool CheckHouseForValue(int row, int column, int value)
    {
        int gridValue;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                gridValue = grid[(((row / 3) * 3) + 1), (((column / 3) * 3) + 1)];
                if (gridValue == value)
                {
                    return true;
                }
            }
        }
        return false;
    }

    List<int> RowAvailable(int row)
    {
        List<int> output = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (rList[row,i])
            {
                output.Add(i + 1);
            }
        }
        Debug.Log("RowAvailable | Row " + row);
        PrintList(output);
        return output;
    }

    List<int> ColumnAvailable(int column)
    {
        List<int> output = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (cList[column,i])
            {
                output.Add(i + 1);
            }
        }
        Debug.Log("ColumnAvailable | Column " + column);
        PrintList(output);
        return output;
    }

    List<int> HouseAvailable(int row, int column)
    {
        int house = 0;
        int rowHouse;
        int columnHouse;
        int houseSwitch;

        rowHouse = (row / 3);
        columnHouse = (column / 3);
        houseSwitch = (rowHouse * 10) + columnHouse;

        switch(houseSwitch) 
        {
            case 00: house = 0; break;
            case 01: house = 1; break;
            case 02: house = 2; break;
            case 10: house = 3; break;
            case 11: house = 4; break;
            case 12: house = 5; break;
            case 20: house = 6; break;
            case 21: house = 7; break;
            case 22: house = 8; break;
            default: break;
        }

        List<int> output = new List<int>();
        for (int i = 0; i < 9; i++)
        {
            if (hList[house,i])
            {
                output.Add(i + 1);
            }
        }
        Debug.Log("HouseAvailable | House " + house);
        PrintList(output);
        return output;
    }

    List<int> CellAvailable(int row, int column)
    {
        List<int> cellList = new List<int>();
        List<int> tempList = new List<int>();
        List<int> columnList = ColumnAvailable(column);
        List<int> rowList = RowAvailable(row);
        List<int> houseList = HouseAvailable(column, row);

        foreach (int num in columnList)
        {
            if (rowList.Contains(num))
            {
                tempList.Add(num);
            }
        }

        foreach (int num in tempList)
        {
            if (houseList.Contains(num))
            {
                cellList.Add(num);
            }
        }

        Debug.Log("CellAvailable | Cell (" + row + "," + column + ")");
        PrintList(cellList);

        return cellList;
    }

    void UpdateRList(int row, int value)
    {
        rList[row,(value - 1)] = false;
        return;
    }

    void UpdateCList(int column, int value)
    {
        cList[column,(value - 1)] = false;
        return;
    }

    void UpdateHList(int column, int row, int value)
    {
        int house = 0;
        int rowHouse;
        int columnHouse;
        int houseSwitch;


        rowHouse = (row / 3);
        columnHouse = (column / 3);
        houseSwitch = (rowHouse * 10) + columnHouse;

        switch(houseSwitch) 
        {
            case 00: house = 0; break;
            case 01: house = 1; break;
            case 02: house = 2; break;
            case 10: house = 3; break;
            case 11: house = 4; break;
            case 12: house = 5; break;
            case 20: house = 6; break;
            case 21: house = 7; break;
            case 22: house = 8; break;
            default: break;
        }
        
        hList[house,(value - 1)] = false;
        //Debug.Log("hList | Remove | " + (value - 1))
        return;
    }

    void UpdateLists(int row, int column, int value)
    {
        UpdateRList(row, value);
        UpdateCList(column, value);
        UpdateHList(row, column, value);
    }

    void FillGrid()
    {
        List<int> cellList = new List<int>();
        int rand;

        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[i,j] == 0)
                {
                    cellList = CellAvailable(i,j);
                    if (cellList.Count == 0)
                    {
                        ResetRow(i);
                        //i--;
                        break;
                    }
                    
                    rand = (int)(Random.value * cellList.Count);
                    grid[i,j] = cellList[rand];
                    Debug.Log("Cell (" + i + "," + j + ") = " + cellList[rand]);
                    UpdateLists(i, j, cellList[rand]);
                    Debug.Log("Current Grid");
                    PrintGrid();
                }
            }
        }
    }

    void ResetRow(int row)
    {
        reset = true;
        for (int i = 0; i < 9; i++)
        {
            grid[row,i] = 0;
        }
        Debug.Log("Reset Row " + row + " | Current Grid");
        PrintGrid();
    }

    void PrintGrid()
    {
        string output = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                output += grid[i,j];
                if (j < 8)
                {
                    output += " ";
                }
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    void PrintList(List<int> list)
    {
        string output = "";
        foreach (int num in list)
        {
            output += num;
            output += " ";
        }
        Debug.Log(output);
    }
}