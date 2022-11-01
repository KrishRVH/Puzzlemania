using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrossSumPuzzle : MonoBehaviour
{
    private GameObject master;
    private int[] numbers;
    public string[] operations;
    public int[] results;
    private bool[] canDivide;
    private Transform numbersTF;
    public string[,] equations;
    private GameObject winPanel;
    private GameObject losePanel;

    public void PlayCrossSum()
    {
        master = GameObject.Find("Master");
        numbers = new int[9];
        operations = new string[12];
        results = new int[6];
        canDivide = new bool[12];
        for (int i = 0; i < 12; i++)
        {
            operations[i] = "";
            canDivide[i] = false;
        }
        for (int i = 0; i < 9; i++)
        {
            numbers[i] = 0;
        }
        for (int i = 0; i < 6; i++)
        {
            results[i] = 0;
        }

        equations = new string[5,5];
        GenerateRandomNumbers();
        CheckFirstHalfDivisionPossibilities();
        GenerateFirstHalfRandomOperations();
        CheckSecondHalfDivisionPossibilities();
        GenerateSecondHalfRandomOperations();
        CalculateResults();
        transform.GetComponent<CrossSumLayout>().StartCrossSum();
        numbersTF = transform.GetChild(2);
        TrackGame();
    }

    private void GenerateRandomNumbers()
    {
        List<int> numberList = new List<int>();
        int[] tempArray = {1, 2, 3, 4, 5, 6, 7, 8, 9};
        numberList.AddRange(tempArray);

        int count = 0;
        while (numberList.Count > 0)
        {
            int rand = (numberList.Count + 1);
            while (rand >= numberList.Count) { rand = ((int)(Random.value * numberList.Count)); }
            numbers[count] = numberList[rand];
            numberList.Remove(numberList[rand]);
            count++;
        }
    }

    private void CheckFirstHalfDivisionPossibilities()
    {
        if (numbers[0] % numbers[1] == 0) { canDivide[0] = true; }
        if (numbers[0] % numbers[3] == 0) { canDivide[2] = true; }
        if (numbers[1] % numbers[4] == 0) { canDivide[3] = true; }
        if (numbers[2] % numbers[5] == 0) { canDivide[4] = true; }
        if (numbers[3] % numbers[4] == 0) { canDivide[5] = true; }
        if (numbers[6] % numbers[7] == 0) { canDivide[10] = true; }
    }

    private void CheckSecondHalfDivisionPossibilities()
    {
        if (GetSubAnswer(numbers[0], operations[0], numbers[1]) % numbers[2] == 0) { canDivide[1] = true; }
        if (GetSubAnswer(numbers[3], operations[5], numbers[4]) % numbers[5] == 0) { canDivide[6] = true; }
        if (GetSubAnswer(numbers[0], operations[2], numbers[3]) % numbers[6] == 0) { canDivide[7] = true; }
        if (GetSubAnswer(numbers[1], operations[3], numbers[4]) % numbers[7] == 0) { canDivide[8] = true; }
        if (GetSubAnswer(numbers[2], operations[4], numbers[5]) % numbers[8] == 0) { canDivide[9] = true; }
        if (GetSubAnswer(numbers[6], operations[10], numbers[7]) % numbers[8] == 0) { canDivide[11] = true; }
        return;
    }

    private int GetSubAnswer(int firstValue, string operation, int secondValue)
    {
        switch(operation)
        {
            case "+": return (firstValue + secondValue);
            case "-": return (firstValue - secondValue);
            case "*": return (firstValue * secondValue);
            case "/": return (firstValue / secondValue);
            default: return 0;
        }
    }

    private void GenerateFirstHalfRandomOperations()
    {
        List<string> operationList1 = new List<string>();
        List<string> operationList2 = new List<string>();
        List<string> operationList3 = new List<string>();
        List<string> operationList4 = new List<string>();

        string option = master.transform.GetComponent<GameState>().gameOption;

        if (option == "0") 
        {
            string[] tempArray1 = {"+", "*"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "*"};
            operationList2.AddRange(tempArray2);
        }
        else if (option == "1")
        {
            string[] tempArray1 = {"+", "-", "*"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "-", "*"};
            operationList2.AddRange(tempArray2);
        }
        else if (option == "2")
        {
            string[] tempArray1 = {"+", "-", "*", "/"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "-", "*"};
            operationList2.AddRange(tempArray2);
        }

        for (int i = 0; i < 12; i ++)
        {
            if ((i == 1) || (i == 11) || ((i > 5) && (i < 10))) { continue; }
            if (canDivide[i])
            {
                int rand = (operationList1.Count + 1);
                while (rand >= operationList1.Count) { rand = ((int)(Random.value * operationList1.Count)); }
                operations[i] = operationList1[rand];
            }
            else if (!canDivide[i])
            {
                int rand = (operationList2.Count + 1);
                while (rand >= operationList2.Count) { rand = ((int)(Random.value * operationList2.Count)); }
                operations[i] = operationList2[rand];
            }
        }
    }

    private void GenerateSecondHalfRandomOperations()
    {
        List<string> operationList1 = new List<string>();
        List<string> operationList2 = new List<string>();
        List<string> operationList3 = new List<string>();
        List<string> operationList4 = new List<string>();

        string option = master.transform.GetComponent<GameState>().gameOption;

        if (option == "0") 
        {
            string[] tempArray1 = {"+", "*"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "*"};
            operationList2.AddRange(tempArray2);
        }
        else if (option == "1")
        {
            string[] tempArray1 = {"+", "-", "*"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "-", "*"};
            operationList2.AddRange(tempArray2);
        }
        else if (option == "2")
        {
            string[] tempArray1 = {"+", "-", "*", "/"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "-", "*"};
            operationList2.AddRange(tempArray2);
        }

        for (int i = 0; i < 12; i ++)
        {
            if ((i == 0) || (i == 10) || ((i > 1) && (i < 6))) { continue; }

            if (canDivide[i])
            {
                int rand = (operationList1.Count + 1);
                while (rand >= operationList1.Count) { rand = ((int)(Random.value * operationList1.Count)); }
                operations[i] = operationList1[rand];
            }
            else if (!canDivide[i])
            {
                int rand = (operationList2.Count + 1);
                while (rand >= operationList2.Count) { rand = ((int)(Random.value * operationList2.Count)); }
                operations[i] = operationList2[rand];
            }
        }
    }

    private void CalculateResults()
    {
        int count = 0;
        for (int i = 0; i <= 6; i += 3)
        {
            results[count] = GetAnswer(numbers[i], operations[((i / 3) * 5)], numbers[(i + 1)], operations[(((i / 3) * 5) + 1)], numbers[(i + 2)]);
            count++;
        }

        for (int i = 0; i < 3; i++)
        {
            results[count] = GetAnswer(numbers[i], operations[(i + 2)], numbers[(i + 3)], operations[(i + 7)], numbers[(i + 6)]);
            count++;
        }
    }

    private int GetAnswer(int firstValue, string firstOperation, int secondValue, string secondOperation, int thirdValue)
    {
        int temp = 0;
        switch(firstOperation)
        {
            case "+": temp = (firstValue + secondValue); break;
            case "-": temp = (firstValue - secondValue); break;
            case "*": temp = (firstValue * secondValue); break;
            case "/": temp = (firstValue / secondValue); break;
            default: break;
        }

        switch(secondOperation)
        {
            case "+": return (temp + thirdValue);
            case "-": return (temp - thirdValue);
            case "*": return (temp * thirdValue);
            case "/": return (temp / thirdValue);
            default: return 0;
        }
    }

    public void CheckSolution()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        for (int i = 0; i < 9; i++)
        {
            if (numbers[i].ToString() != numbersTF.GetChild(i).GetComponent<CrossSumNumber>().GetCurrentValue())
            {
                return;
            }
        }
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "WinPanel")
            {
                winPanel = temp;
                break;
            }
        }
        TrackWin();
        winPanel.SetActive(true);
    }

    private int GetNumber(int index)
    {
        return int.Parse(numbersTF.GetChild(index).GetComponent<CrossSumNumber>().GetCurrentValue());
    }

    public void ValidateRowResult(int index)
    {
        if (results[(index / 3)] == GetAnswer(GetNumber(index), operations[((index / 3) * 5)], GetNumber((index + 1)), operations[(((index / 3) * 5) + 1)], GetNumber((index + 2))))
        {
            transform.GetChild(4).GetChild((index / 3)).GetComponent<CrossSumResult>().ShowValidResult();
        }
        else
        {
            transform.GetChild(4).GetChild((index / 3)).GetComponent<CrossSumResult>().ShowInvalidResult();
        }
    }

    public void ValidateColumnnResult(int index)
    {
        if (results[((index % 3) + 3)] == GetAnswer(GetNumber(index), operations[(index + 2)], GetNumber((index + 3)), operations[(index + 7)], GetNumber((index + 6))))
        {
            transform.GetChild(4).GetChild(((index % 3) + 3)).GetComponent<CrossSumResult>().ShowValidResult();
        }
        else
        {
            transform.GetChild(4).GetChild(((index % 3) + 3)).GetComponent<CrossSumResult>().ShowInvalidResult();
        }
    }

    private void TrackGame()
    {
        StartCoroutine(WaitTenSeconds());
        int temp = PlayerPrefs.GetInt("CrossSumGamesPlayed", 0);
        PlayerPrefs.SetInt("CrossSumGamesPlayed", (temp + 1));
        PlayerPrefs.Save();
    }

    IEnumerator WaitTenSeconds()
    {
        yield return new WaitForSecondsRealtime(10);
    }

    private void TrackWin()
    {
        int temp = PlayerPrefs.GetInt("CrossSumGamesWon", 0);
        PlayerPrefs.SetInt("CrossSumGamesWon", (temp + 1));
        PlayerPrefs.Save();
    }
}