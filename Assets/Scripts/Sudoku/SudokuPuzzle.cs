using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SudokuPuzzle : MonoBehaviour
{

    public class RandomCell
    {
        public int Row;
        public int Column;

        public RandomCell(int Row, int Column)
        {
            this.Row = Row;
            this.Column = Column;
        }
    }

    private GameObject master;
    private int sudokuGridSize;
    private int sudokuHouseSize;
    private int sudokuDifficulty;
    private int[,] sudokuGrid;
    private int[,] solvedGrid;
    private List<RandomCell> randomCellList;
    GameObject winPanel;
    public List<int> digitCount;

    public void PlaySudoku()
    {
        master = GameObject.Find("Master");
        sudokuGridSize = 9;

        digitCount = new List<int>();
        int[] tempArray = {0, 0, 0, 0, 0, 0, 0, 0, 0};
        digitCount.AddRange(tempArray);

        sudokuHouseSize = (int)(Mathf.Sqrt(sudokuGridSize));
        randomCellList = new List<RandomCell>();

        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                RandomCell temp = new RandomCell(i,j);
                randomCellList.Add(temp);
            }
        }

        solvedGrid = new int[sudokuGridSize,sudokuGridSize];
        sudokuGrid = new int[sudokuGridSize,sudokuGridSize];

        string option = master.transform.GetComponent<GameState>().gameOption;
        if (option == "0") { sudokuDifficulty = 25; }
        else if (option == "1") { sudokuDifficulty = 45; } 
        else if (option == "2") { sudokuDifficulty = 65; }
        ResetGrid();
        InitialGridFill();
        int count = 0;
        while (!SolveGrid())
        {
            count++;
            if (count >= 10)
            {
                //Debug.Log("Break 1");
                break;
            }
            //Debug.Log("Failed SolveGrid");
            ResetGrid();
            InitialGridFill();
        }
        CopyGrid(sudokuGrid,solvedGrid);
        RemoveCells();

        transform.GetComponent<SudokuLayout>().StartSudoku();

        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                if (sudokuGrid[i,j] != 0)
                {
                    UpdateDigitCount((sudokuGrid[i,j] - 1), true);
                }
            }
        }
    }

    public void UpdateDigitCount(int index, bool add)
    {
        if (add)
        {
            digitCount[index] += 1;
            if (digitCount[index] == 9)
            {
                transform.GetChild(9).GetChild(index).GetComponent<SudokuToggleDigit>().DisableToggle(true);
                for (int i = (index + 1); i < 11; i++)
                {
                    if (transform.GetChild(9).GetChild(i).GetComponent<SudokuToggleDigit>().IsDisabled())
                    {
                        continue;
                    }
                    transform.GetChild(9).GetChild(i).GetComponent<SudokuToggleDigit>().Toggle(true);
                    break;
                }
            }
        }
        else
        {
            digitCount[index] -= 1;
            transform.GetChild(9).GetChild(index).GetComponent<SudokuToggleDigit>().DisableToggle(false);
        }
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
                        userGrid[(j + (i * 3)), columnCount] = int.Parse(house.GetChild((k + (j * 3))).GetComponent<SudokuCell>().GetCurrentValue());
                        columnCount++;
                    }
                }
            }
        }


        for (int i = 0; i < sudokuGridSize; i++)
        {
            for (int j = 0; j < sudokuGridSize; j++)
            {
                if (solvedGrid[i,j] != userGrid[i,j])
                {
                    return;
                }
            }
        }

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "WinPanel")
            {
                winPanel = temp;
                break;
            }
        }
        winPanel.SetActive(true);
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
        int rand = 0;
        if (column >= sudokuGridSize) { row++; column = 0; }

        if (row >= sudokuGridSize) { return true; }

        if (sudokuGrid[row,column] != 0) { return SolveGrid(row, (column + 1)); }

        List<int> cellList = CellAvailable(row, column);
        while (cellList.Count > 0)
        {
            rand = (cellList.Count + 1);
            while (rand >= cellList.Count) { rand = (int)(Random.value * cellList.Count); }
            sudokuGrid[row,column] = cellList[rand];
            cellList.RemoveAt(rand);
            if (SolveGrid(row, (column + 1))) { return true; }
            sudokuGrid[row,column] = 0;
            //cellList.RemoveAt(rand);
        }
        return false;
    }

    RandomCell GetRandomCell(int rand)
    {
        return randomCellList[rand];
    }

    int GetRandomCellCount()
    {
        return randomCellList.Count;
    }

    void RemoveRandomCell(int rand)
    {
        randomCellList.RemoveAt(rand);
        return;
    }
    

    void RemoveCells()
    {
        int cellValue = 0;
        int cellsRemoved = 0;
        int rand = 0;
        List<int> numbers = new List<int>();
        bool unique = true;
        int[,] tempGrid = new int[sudokuGridSize,sudokuGridSize];

        //int count = 0;
        while ((cellsRemoved < sudokuDifficulty) && (GetRandomCellCount() > 0))
        {
            //count++;
            //if (count >= 90) { Debug.Log("Break 2"); break; }

            rand = GetRandomCellCount() + 1;
            while (rand >= GetRandomCellCount()) { rand = (int)(Random.value * GetRandomCellCount()); }
            RandomCell tempRandomNumber = GetRandomCell(rand);
            RemoveRandomCell(rand);
            cellValue = sudokuGrid[tempRandomNumber.Row,tempRandomNumber.Column];
            sudokuGrid[tempRandomNumber.Row,tempRandomNumber.Column] = 0;
            numbers = CellAvailable(tempRandomNumber.Row, tempRandomNumber.Column);
            if (numbers.Count <= 1)
            {
                sudokuGrid[tempRandomNumber.Row,tempRandomNumber.Column] = 0;
                cellsRemoved++;
                continue;
            }

            numbers.Remove(cellValue);
            unique = true;
            CopyGrid(sudokuGrid, tempGrid);
            foreach (int num in numbers)
            {
                CopyGrid(tempGrid, sudokuGrid);
                sudokuGrid[tempRandomNumber.Row,tempRandomNumber.Column] = num;
                if (SolveGrid())
                {
                    // More than one solution
                    CopyGrid(tempGrid, sudokuGrid);
                    sudokuGrid[tempRandomNumber.Row,tempRandomNumber.Column] = cellValue;
                    unique = false;
                    break;
                }
            }
            if (unique)
            {
                sudokuGrid[tempRandomNumber.Row,tempRandomNumber.Column] = 0;
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