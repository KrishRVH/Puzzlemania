using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrossSumPuzzle : MonoBehaviour
{
    private GameObject master;
    private List<int> numbers;
    public List<string> operations;
    public List<int> results;
    private List<bool> canDivide;
    //private List<bool> canSubtract;
    private Transform numbersTF;
    public string[,] equations;
    private GameObject winPanel;
    private GameObject losePanel;

    public void PlayCrossSum()
    {
        master = GameObject.Find("Master");
        numbers = new List<int>();
        operations = new List<string>();
        results = new List<int>();
        canDivide = new List<bool>();
        //canSubtract = new List<bool>();
        equations = new string[5,5];
        GenerateRandomNumbers();
        CheckDivisionPossibilities();
        //CheckSubtractionPossibilities();
        GenerateRandomOperations();
        CalculateResults();
        transform.GetComponent<CrossSumLayout>().StartCrossSum();
        numbersTF = transform.GetChild(2);
    }

    private void GenerateRandomNumbers()
    {
        List<int> numberList = new List<int>();
        int[] tempArray = {1, 2, 3, 4, 5, 6, 7, 8, 9};
        numberList.AddRange(tempArray);

        while (numberList.Count > 0)
        {
            int rand = (numberList.Count + 1);
            while (rand >= numberList.Count) { rand = ((int)(Random.value * numberList.Count)); }
            numbers.Add(numberList[rand]);
            numberList.Remove(numberList[rand]);
        }
    }

    private void CheckDivisionPossibilities()
    {
        if (numbers[0] % numbers[1] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[1] % numbers[2] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[0] % numbers[3] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[1] % numbers[4] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[2] % numbers[5] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[3] % numbers[4] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[4] % numbers[5] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[3] % numbers[6] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[4] % numbers[7] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[5] % numbers[8] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[6] % numbers[7] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        if (numbers[7] % numbers[8] == 0) { canDivide.Add(true); } else { canDivide.Add(false); }
        return;
    }

    /*
    private void CheckSubtractionPossibilities()
    {
        if (numbers[0] - numbers[1] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if ((numbers[0] - numbers[1]) - numbers[2] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if (numbers[0] - numbers[3] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if (numbers[1] - numbers[4] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if (numbers[2] - numbers[5] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if (numbers[3] - numbers[4] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if ((numbers[3] - numbers[4]) - numbers[5] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if ((numbers[0] - numbers[3]) - numbers[6] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if ((numbers[1] - numbers[4]) - numbers[7] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if ((numbers[0] - numbers[5]) - numbers[8] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if (numbers[6] - numbers[7] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        if ((numbers[6] - numbers[7]) - numbers[8] >= 0) { canSubtract.Add(true); } else { canSubtract.Add(false); }
        return;
    }
    */

    private void GenerateRandomOperations()
    {
        List<string> operationList1 = new List<string>();
        List<string> operationList2 = new List<string>();
        List<string> operationList3 = new List<string>();
        List<string> operationList4 = new List<string>();

        string option = master.transform.GetComponent<GameState>().gameOption;

        /*
        if (option == "0") 
        {
            string[] tempArray1 = {"+", "*"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "*"};
            operationList2.AddRange(tempArray2);

            string[] tempArray3 = {"+", "*"};
            operationList3.AddRange(tempArray3);

            string[] tempArray4 = {"+", "*"};
            operationList4.AddRange(tempArray4);
        }
        else if (option == "1")
        {
            string[] tempArray1 = {"+", "-", "*"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "-", "*"};
            operationList2.AddRange(tempArray2);

            string[] tempArray3 = {"+", "*"};
            operationList3.AddRange(tempArray3);

            string[] tempArray4 = {"+", "*"};
            operationList4.AddRange(tempArray4);
        }
        else if (option == "2")
        {
            string[] tempArray1 = {"+", "-", "*", "/"};
            operationList1.AddRange(tempArray1);

            string[] tempArray2 = {"+", "-", "*"};
            operationList2.AddRange(tempArray2);

            string[] tempArray3 = {"+", "*", "/"};
            operationList3.AddRange(tempArray3);

            string[] tempArray4 = {"+", "*"};
            operationList4.AddRange(tempArray4);
        }
        */

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
            /*
            if (canDivide[i] && canSubtract[i])
            {
                int rand = (operationList1.Count + 1);
                while (rand >= operationList1.Count) { rand = ((int)(Random.value * operationList1.Count)); }
                operations.Add(operationList1[rand]);
            }
            else if (!canDivide[i] && canSubtract[i])
            {
                int rand = (operationList2.Count + 1);
                while (rand >= operationList2.Count) { rand = ((int)(Random.value * operationList2.Count)); }
                operations.Add(operationList2[rand]);
            }
            else if (canDivide[i] && !canSubtract[i])
            {
                int rand = (operationList3.Count + 1);
                while (rand >= operationList3.Count) { rand = ((int)(Random.value * operationList3.Count)); }
                operations.Add(operationList3[rand]);
            }
            else if (!canDivide[i] && !canSubtract[i])
            {
                int rand = (operationList4.Count + 1);
                while (rand >= operationList4.Count) { rand = ((int)(Random.value * operationList4.Count)); }
                operations.Add(operationList4[rand]);
            }
            */

            if (canDivide[i])
            {
                int rand = (operationList1.Count + 1);
                while (rand >= operationList1.Count) { rand = ((int)(Random.value * operationList1.Count)); }
                operations.Add(operationList1[rand]);
            }
            else if (!canDivide[i])
            {
                int rand = (operationList2.Count + 1);
                while (rand >= operationList2.Count) { rand = ((int)(Random.value * operationList2.Count)); }
                operations.Add(operationList2[rand]);
            }
        }
    }

    private void CalculateResults()
    {
        for (int i = 0; i <= 6; i += 3)
        {
            results.Add(GetAnswer(numbers[i], operations[((i / 3) * 5)], numbers[(i + 1)], operations[(((i / 3) * 5) + 1)], numbers[(i + 2)]));
        }

        for (int i = 0; i < 3; i++)
        {
            results.Add(GetAnswer(numbers[i], operations[(i + 2)], numbers[(i + 3)], operations[(i + 7)], numbers[(i + 6)]));
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
}