using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{

    public string toggleValue;
    public string inputType;
    private double houseTemp;
    private double indexTemp;
    public bool[,] columns;
    public bool[,] rows;
    public bool[,] houses;
    private int output;

    // Variables used for creating random sudoku puzzle
    private bool[,] randomColumns;
    private bool[,] randomRows;
    private bool[,] randomHouses;
    private int[,] randomGrid;
    private int randomColumn;
    private int randomRow;
    private int randomHouse;
    private int randomIndex;
    private int randomValue;
    private bool gridFilled;

    // Start is called before the first frame update
    void Start()
    {
        toggleValue = "";
        inputType = "numbers";
        columns = new bool[9,9];
        rows = new bool[9,9];
        houses = new bool[9,9];
        randomColumns = new bool[9,9];
        randomRows = new bool[9,9];
        randomHouses = new bool[9,9];
        randomGrid = new int[9,9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                columns[i,j] = false;
                rows[i,j] = false;
                houses[i,j] = false;
                randomColumns[i,j] = false;
                randomRows[i,j] = false;
                randomHouses[i,j] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateGrid()
    {
        
    }

    public void GenerateFullGrid()
    {
        Debug.Log("Start");
        randomHouse = 1;
        randomIndex = 1;
        randomColumn = CalculateColumn(randomHouse, randomIndex);
        randomRow = CalculateRow(randomHouse, randomIndex);
        gridFilled = false;
        
        while (!gridFilled)
        {
            Debug.Log("While");
            randomValue = Random.Range(1,9);
            if (RandomCheckColumnValue(randomColumns, randomColumn, randomValue))
            {
                Debug.Log("Invalid Column, Continue");
                continue;
            }
            else if (RandomCheckRowValue(randomRows, randomRow, randomValue))
            {
                Debug.Log("Invalid Row, Continue");
                continue;
            }
            else if (RandomCheckHouseValue(randomHouses, randomHouse, randomValue))
            {
                Debug.Log("Invalid House, Continue");
                continue;
            }
            else
            {
                Debug.Log("House: " + randomHouse + " | Index: " + randomIndex + " | Value: " + randomValue);
                randomGrid[(randomRow - 1),(randomColumn - 1)] = randomValue;
                randomColumns[(randomColumn - 1),(randomValue - 1)] = true;
                randomRows[(randomRow - 1),(randomValue - 1)] = true;
                randomHouses[(randomHouse - 1),(randomValue - 1)] = true;
                randomIndex++;
                if (randomIndex == 10)
                {
                    randomHouse++;
                    randomIndex = 1;
                }
                randomColumn = CalculateColumn(randomHouse, randomIndex);
                randomRow = CalculateRow(randomHouse, randomIndex);
            }

            foreach (bool value in randomColumns)
            {
                if (!value)
                {
                    Debug.Log("Columns not filled, Continue");
                    gridFilled = false;
                    break;
                }
                else
                {
                    gridFilled = true;
                }
            }

            if (!gridFilled)
            {
                continue;
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++) {
                    System.Console.Write("{0} ", randomGrid[i, j]);
                }
                System.Console.WriteLine();
            }
        }
    }

    public int CalculateColumn(int house, int index)
    {
        houseTemp = house % 3;
        indexTemp = (index % 3) / 10.0;
        switch(houseTemp + indexTemp) 
        {
            case 0:
                output = 9;
                break;
            case 0.1:
                output = 7;
                break;
            case 0.2:
                output = 8;
                break;
            case 1:
                output = 3;
                break;
            case 1.1:
                output = 1;
                break;
            case 1.2:
                output = 2;
                break;
            case 2:
                output = 6;
                break;
            case 2.1:
                output = 4;
                break;
            case 2.2:
                output = 5;
                break;
            default:
                break;
        }
        return output;
    }

    public int CalculateRow(int house, int index)
    {
        houseTemp = Mathf.Floor((house - 1) / 3);
        indexTemp = Mathf.Floor((index - 1) / 3) / 10.0;
        switch(houseTemp + indexTemp)
        {
            case 0:
                output = 1;
                break;
            case 0.1:
                output = 2;
                break;
            case 0.2:
                output = 3;
                break;
            case 1:
                output = 4;
                break;
            case 1.1:
                output = 5;
                break;
            case 1.2:
                output = 6;
                break;
            case 2:
                output = 7;
                break;
            case 2.1:
                output = 8;
                break;
            case 2.2:
                output = 9;
                break;
            default:
                break;
        }
        return output;
    }

    public void UpdateCellValue(int column, int row, int house, int value, bool add)
    {
        // Column: 1-9
        // Row: 1-9
        // House: 1-9
        // Value: 1-9
        // Add: True = Add; False = Remove;
        columns[(column - 1),(value - 1)] = add;
        rows[(row - 1),(value - 1)] = add;
        houses[(house - 1),(value - 1)] = add;

    }

    public bool CheckColumnValue(int column, int value)
    {
        return columns[(column - 1),(value - 1)];
    }

    public bool CheckRowValue(int row, int value)
    {
        return rows[(row - 1),(value - 1)];
    }

    public bool CheckHouseValue(int house, int value)
    {
        return houses[(house - 1),(value - 1)];
    }

    public bool RandomCheckColumnValue(bool[,] columns, int column, int value)
    {
        return columns[(column - 1),(value - 1)];
    }

    public bool RandomCheckRowValue(bool[,] rows, int row, int value)
    {
        return rows[(row - 1),(value - 1)];
    }

    public bool RandomCheckHouseValue(bool[,] houses, int house, int value)
    {
        return houses[(house - 1),(value - 1)];
    }

    public void UpdateToggle(string input)
    {
        toggleValue = input;
    }

    public void UpdateInputType(string input)
    {
        inputType = input;
    }
}
