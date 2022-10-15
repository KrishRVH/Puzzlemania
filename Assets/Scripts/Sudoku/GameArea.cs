using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{

    public string toggleValue;
    public string inputType;
    public bool[,] columns;
    public bool[,] rows;
    public bool[,] houses;


    // Start is called before the first frame update
    void Start()
    {
        toggleValue = "";
        inputType = "numbers";
        columns = new bool[9,9];
        rows = new bool[9,9];
        houses = new bool[9,9];
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 9; j++)
            {
                columns[i,j] = false;
                rows[i,j] = false;
                houses[i,j] = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void UpdateToggle(string input)
    {
        toggleValue = input;
    }

    public void UpdateInputType(string input)
    {
        inputType = input;
    }
}
