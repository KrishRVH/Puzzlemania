using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuPuzzle : MonoBehaviour
{
    public int sudokuGridSize = 9;
    public int sudokuHouseSize;
    public int[,] sudokuGrid = new int[sudokuGridSize,sudokuGridSize];

    void Start()
    {
        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                sudokuGrid[i,j] = 0
            }
        }

        sudokuHouseSize = (int)(Mathf.Sqrt(sudokuGridSize));
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
            default: return null;
        }
    }

    public bool IsValidPlacement(int row, int column, int value)
    {
        if (!IsValidRowPlacement(row, value)) { return false;}
        else if (!IsValidRowPlacement(column, value)) { return false; }
        else if (!IsValidHousePlacement(CalculateHouse(row, column), value)) { return false; }
        return true;
    }

    public bool IsValidRowPlacement(int row, int value)
    {
        for (int i = 0; i < sudokuGridSize; i++)
        {
            if (value == sudokuGrid[row,i])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsValidColumnPlacement(int column, int value)
    {
        for (int i = 0; i < sudokuGridSize; i++)
        {
            if (value == sudokuGrid[i,column])
            {
                return false;
            }
        }
        return true;
    }

    public bool IsValidHousePlacement(int house, int value)
    {
        for (int i = 0; i < sudokuHouseSize; i++)
        {
            for (int j = 0; j < sudokuHouseSize; j++)
            {
                if (value == sudokuGrid[(((row / sudokuHouseSize) * sudokuHouseSize) + i), (((column / sudokuHouseSize) * sudokuHouseSize) + j)])
                {
                    return false;
                }
            }
        }
        return true;
    }
}