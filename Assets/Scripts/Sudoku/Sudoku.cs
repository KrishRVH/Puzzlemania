using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sudoku : MonoBehaviour
{
    public int[,] grid = new int[9,9];
    private bool[,] cList;
    private bool[,] rList;
    private bool[,] hList;
    private Button hintButton;
    private bool reset = false;
    //private int resetCount = 0;
    private bool once = true;

    void Start()
    {

    }

    void ResetGrid()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                grid[i,j] = 0;
            }
        }
    }

    public void SudokuStart()
    {
        if (once)
        {
            once = false;
            //hintButton = GetComponent<Button>();
            //hintButton.onClick.AddListener(SudokuStart);
            //grid = new int[9,9];
            ResetGrid();
            //Debug.Log ("0 Grid");
            //PrintIntGrid(grid);
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
        }

        FillGrid();
        //Debug.Log("---------Pre-Solve-------------");
        //PrintIntGrid(grid);
        if (SolveGrid(0, 0))
        {
            Debug.Log("True");
            PrintIntGrid(grid);
            reset = false;
        }
        else
        {
            ResetGrid();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cList[i,j] = true;
                    rList[i,j] = true;
                    hList[i,j] = true;
                }
            }
            SudokuStart();
        }
        
        if (reset) {
            //resetCount++;
            reset = false;
            //if (resetCount < 200)
            //{
            SudokuStart();
            //}
            //else
            //{
                //Debug.Log("Reset Limit Reached");
                //Debug.Log("Pre-Solve");
                //PrintIntGrid(grid);
                //SolveGrid();
            //}
        }
        return;
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

    bool CheckAllForValue(int row, int column, int value)
    {
        if (CheckRowForValue(row, value))
        {
            return true;
        }

        if (CheckColumnForValue(column, value))
        {
            return true;
        }

        if (CheckHouseForValue(row, column, value))
        {
            return true;
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
        //Debug.Log("RowAvailable | Row " + row);
        //PrintList(output);
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
        //Debug.Log("ColumnAvailable | Column " + column);
        //PrintList(output);
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
        //Debug.Log("HouseAvailable | House " + house);
        //PrintList(output);
        return output;
    }

    List<int> CellAvailable(int row, int column)
    {
        List<int> cellList = new List<int>();
        List<int> tempList = new List<int>();
        List<int> rowList = RowAvailable(row);
        List<int> columnList = ColumnAvailable(column);
        List<int> houseList = HouseAvailable(row, column);

        foreach (int num in rowList)
        {
            if (columnList.Contains(num))
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

        //Debug.Log("CellAvailable | Cell (" + row + "," + column + ")");
        //PrintList(cellList);

        return cellList;
    }

    void UpdateRList(int row, int value, bool remove)
    {
        rList[row,(value - 1)] = !remove;
        //Debug.Log("Current rList");
        //PrintBoolGrid(rList);
        return;
    }

    void UpdateCList(int column, int value, bool remove)
    {
        cList[column,(value - 1)] = !remove;
        //Debug.Log("Current cList");
        //PrintBoolGrid(cList);
        return;
    }

    void UpdateHList(int row, int column, int value, bool remove)
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
        
        hList[house,(value - 1)] = !remove;
        //Debug.Log("hList | Remove | " + (value - 1))
        //Debug.Log("Current hList");
        //PrintBoolGrid(hList);
        return;
    }

    void UpdateLists(int row, int column, int value, bool remove)
    {
        UpdateRList(row, value, remove);
        UpdateCList(column, value, remove);
        UpdateHList(row, column, value, remove);
    }

    void FillGrid()
    {
        List<int> cellList = new List<int>();
        int rand;

        for (int i = 0; i < 4; i++)
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
                    //Debug.Log("Cell (" + i + "," + j + ") = " + cellList[rand]);
                    grid[i,j] = cellList[rand];
                    UpdateLists(i, j, cellList[rand], true);
                    //Debug.Log("Current Grid");
                    //PrintIntGrid(grid);
                }
            }
        }
    }

    void ResetRow(int row)
    {
        reset = true;
        for (int i = 0; i < 9; i++)
        {
            if (grid[row,i] != 0)
            {
                UpdateLists(row, i, grid[row,i], false);
                grid[row,i] = 0;
            }
        }
        //Debug.Log("Reset Row " + row + " | Current Grid");
        //PrintIntGrid(grid);
    }

    bool SolveGrid(int row, int column)
    {

        if (IsSolved())
        {
            //Debug.Log("Solved Grid");
            //PrintIntGrid(grid);
            return true;
        }

        //Debug.Log("Current Grid");
        //PrintIntGrid(grid);

        if (grid[row,column] != 0)
        {
            if (column == 8) {
                SolveGrid((row + 1), 0);
            }
            else
            {
                SolveGrid(row, (column + 1));
            }
        }

        List<int> cellList = new List<int>();
        cellList = CellAvailable(row, column);
        for (int i = 0; i < cellList.Count; i++)
        {
            if (!CheckAllForValue(row, column, cellList[i]))
            {
                grid[row,column] = cellList[i];
                UpdateLists(row, column, cellList[i], true);
                if (column == 8) {
                    if (SolveGrid((row + 1), 0))
                    {
                        return true;
                    }
                }
                else
                {
                    if (SolveGrid(row, (column + 1)))
                    {
                        return true;
                    }
                }
                UpdateLists(row, column, cellList[i], false);
            }
        }
        if (IsSolved())
        {
            //Debug.Log("Solved Grid");
            //PrintIntGrid(grid);
            return true;
        }
        return false;

    }

    bool IsSolved()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                if (grid[i,j] == 0)
                {
                    //Debug.Log("IsSolved | False");
                    return false;
                }
            }
        }
        //Debug.Log("IsSolved | True");
        return true;
    }

    void PrintIntGrid(int[,] gridToPrint)
    {
        string output = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                output += gridToPrint[i,j];
                if (j < 8)
                {
                    output += " ";
                }
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    void PrintBoolGrid(bool[,] gridToPrint)
    {
        string output = "";
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                output += gridToPrint[i,j];
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