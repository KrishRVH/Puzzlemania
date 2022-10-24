using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuPuzzle : MonoBehaviour
{
    private int sudokuGridSize;
    private int sudokuHouseSize;
    private int sudokuDifficulty;
    private int[,] sudokuGrid;
    private int[,] solvedGrid;
    public SudokuPuzzle sudokuPuzzle;

    public void PlaySudoku()
    {
        sudokuGridSize = 9;
        sudokuHouseSize = 3;
        sudokuDifficulty = 40;
        sudokuPuzzle = new SudokuPuzzle(sudokuGridSize, sudokuDifficulty);
        sudokuPuzzle.InitialGridFill();
        int count = 0;
        while (!sudokuPuzzle.SolveGrid())
        {
            count++;
            if (count >= 10)
            {
                Debug.Log("Break 1");
                break;
            }
            Debug.Log("Failed SolveGrid");
            sudokuPuzzle.ResetGrid();
            sudokuPuzzle.InitialGridFill();
        }
        sudokuPuzzle.CopyGrid(sudokuPuzzle.sudokuGrid,sudokuPuzzle.solvedGrid);
        sudokuPuzzle.RemoveCells();
        transform.GetComponent<SudokuLayout>().StartSudoku();
    }

	public SudokuPuzzle(int sudokuGridSize, int sudokuDifficulty)
	{
        this.sudokuGridSize = sudokuGridSize;
        this.sudokuDifficulty = sudokuDifficulty;

        sudokuHouseSize = (int)(Mathf.Sqrt(sudokuGridSize));

        solvedGrid = new int[sudokuGridSize,sudokuGridSize];
        sudokuGrid = new int[sudokuGridSize,sudokuGridSize];
        ResetGrid();
	}

    public void CheckSolution()
    {
        int[,] userGrid = new int[sudokuGridSize,sudokuGridSize];
        Transform house;
        List<int> houseList = new List<int>();
        int columnCount = 0;
        for (int i = 0; i < 3; i++)
        {
            houseList.Clear();
            houseList.Add(0 + (i * 3));
            houseList.Add(1 + (i * 3));
            houseList.Add(2 + (i * 3));
            for (int j = 0; j < 3; j++)
            {
                columnCount = 0;
                foreach (int item in houseList)
                {
                    house = transform.GetChild(item);
                    for (int k = 0; k < 3; k++)
                    {
                        //Debug.Log(i + " " + j + " " + item + " " + (k + (j * 3)));
                        userGrid[(j + (i * 3)), columnCount] = int.Parse(house.GetChild((k + (j * 3))).GetComponent<SudokuCell>().GetCurrentValue());
                        columnCount++;
                    }
                }
            }
        }

        //PrintOtherGrid(sudokuPuzzle.solvedGrid);
        //PrintOtherGrid(userGrid);

        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                if (sudokuPuzzle.solvedGrid[i,j] != userGrid[i,j])
                {
                    Debug.Log("Incorrect");
                    return;
                }
            }
        }
        Debug.Log("Winner");
        GameObject.Find("WinPanel").SetActive(true);
    }

    public int GetCellValue(int row, int column)
    {
        return sudokuGrid[row,column];
    }

    void ResetGrid()
    {
        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                sudokuGrid[i,j] = 0;
            }
        }
    }

    public int CalculateHouse(int row, int column)
    {
        switch(((row / 3) * 10) + (column / 3)) 
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

    bool IsValidPlacement(int row, int column, int value)
    {
        if (!IsValidRowPlacement(row, value)) { return false;}
        else if (!IsValidColumnPlacement(column, value)) { return false; }
        else if (!IsValidHousePlacement(row, column, value)) { return false; }
        return true;
    }

    bool IsValidRowPlacement(int row, int value)
    {
        for (int i = 0; i < sudokuGridSize; i++)
        {
            if (value == sudokuGrid[row,i]) { return false; }
        }
        return true;
    }

    bool IsValidColumnPlacement(int column, int value)
    {
        for (int i = 0; i < sudokuGridSize; i++)
        {
            if (value == sudokuGrid[i,column]) { return false; }
        }
        return true;
    }

    bool IsValidHousePlacement(int row, int column, int value)
    {
        for (int i = 0; i < sudokuHouseSize; i++)
        {
            for (int j = 0; j < sudokuHouseSize; j++)
            {
                if (value == sudokuGrid[(((int)(row / sudokuHouseSize) * sudokuHouseSize) + i), (((int)(column / sudokuHouseSize) * sudokuHouseSize) + j)])
                {
                    return false;
                }
            }
        }
        return true;
    }

    List<int> RowAvailable(int row)
    {
        List<int> output = new List<int>();
        for (int i = 1; i <= sudokuGridSize; i++)
        {
            if (IsValidRowPlacement(row, i)) { output.Add(i); }
        }
        return output;
    }

    List<int> ColumnAvailable(int column)
    {
        List<int> output = new List<int>();
        for (int i = 1; i <= sudokuGridSize; i++)
        {
            if (IsValidColumnPlacement(column, i)) { output.Add(i); }
        }
        return output;
    }

    List<int> HouseAvailable(int row, int column)
    {
        List<int> output = new List<int>();
        for (int i = 1; i <= sudokuGridSize; i++)
        {
            if (IsValidHousePlacement(row, column, i)) { output.Add(i); }
        }
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
            if (columnList.Contains(num)) { tempList.Add(num); }
        }

        foreach (int num in tempList)
        {
            if (houseList.Contains(num)) { cellList.Add(num); }
        }

        return cellList;
    }

    void InitialGridFill()
    {
        // Filling in Houses 1,5,9
        FillHouse(0,0);
        FillHouse(3,3);
        FillHouse(6,6);
    }

    void FillHouse(int row, int column)
    {
        List<int> numbers = new List<int>();
        numbers = HouseAvailable(row, column);

        int rand = 0;
        for (int i = 0; i < sudokuHouseSize; i++)
        {
            for (int j = 0; j < sudokuHouseSize; j++)
            {
                rand = numbers.Count + 1;
                while (rand >= numbers.Count) { rand = (int)(Random.value * numbers.Count); }
                sudokuGrid[(((row / sudokuHouseSize) * sudokuHouseSize) + i), (((column / sudokuHouseSize) * sudokuHouseSize) + j)] = numbers[rand];
                numbers.Remove(numbers[rand]);
            }
        }
    }

    bool SolveGrid(int row = 0, int column = 0)
    {

        if (column >= sudokuGridSize) { row++; column = 0; }

        if (row >= sudokuGridSize) { return true; }

        if (sudokuGrid[row,column] != 0) { return SolveGrid(row, (column + 1)); }

        List<int> cellList = CellAvailable(row, column);
        foreach (int num in cellList)
        {
            sudokuGrid[row,column] = num;
            if (SolveGrid(row, (column + 1))) { return true; }
            sudokuGrid[row,column] = 0;
        }
        return false;
    }

    void RemoveCells()
    {
        int cellValue = 0;
        int randomRow = 0;
        int randomColumn = 0;
        int cellsRemoved = 0;
        List<int> numbers = new List<int>();
        bool unique = true;
        int[,] tempGrid = new int[sudokuGridSize,sudokuGridSize];

        int count = 0;
        while (cellsRemoved < sudokuDifficulty)
        {
            //PrintGrid();
            //ZeroCounter();
            count++;
            if (count >= 150) { Debug.Log("Break 2"); break; }

            randomRow = sudokuGridSize + 1;
            randomColumn = sudokuGridSize + 1;
            while (randomRow >= sudokuGridSize) { randomRow = (int)(Random.value * sudokuGridSize); }

            while (randomColumn >= sudokuGridSize) { randomColumn = (int)(Random.value * sudokuGridSize); }
            
            if (sudokuGrid[randomRow,randomColumn] == 0) { continue; }

            cellValue = sudokuGrid[randomRow,randomColumn];
            sudokuGrid[randomRow,randomColumn] = 0;
            numbers = CellAvailable(randomRow, randomColumn);
            if (numbers.Count <= 1)
            {
                sudokuGrid[randomRow,randomColumn] = 0;
                cellsRemoved++;
                continue;
            }

            numbers.Remove(cellValue);
            unique = true;
            CopyGrid(sudokuGrid, tempGrid);
            foreach (int num in numbers)
            {
                CopyGrid(tempGrid, sudokuGrid);
                sudokuGrid[randomRow,randomColumn] = num;
                if (SolveGrid())
                {
                    // More than one solution
                    CopyGrid(tempGrid, sudokuGrid);
                    sudokuGrid[randomRow,randomColumn] = cellValue;
                    unique = false;
                    break;
                }
            }
            if (unique)
            {
                sudokuGrid[randomRow,randomColumn] = 0;
                cellsRemoved++;
            }
        }
    }

    void PrintGrid()
    {
        string output = "";
        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                output += sudokuGrid[i,j];
                output += " ";
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    void PrintOtherGrid(int[,] gridToPrint)
    {
        string output = "";
        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                output += gridToPrint[i,j];
                output += " ";
            }
            output += "\n";
        }
        Debug.Log(output);
    }

    void PrintList(List<int> list)
    {
        string output = "";
        foreach (int num in list) { output += num + " "; }
        Debug.Log(output);
    }

    void ZeroCounter()
    {
        int output = 0;
        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                if (sudokuGrid[i,j] == 0) { output++; }
            }
        }
        Debug.Log("Zeroes: " + output);
    }

    void CopyGrid(int[,] gridFrom, int[,] gridTo)
    {
        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                gridTo[i,j] = gridFrom[i,j];
            }
        }
    }

}