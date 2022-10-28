using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumLayout : MonoBehaviour
{

    public GameObject togglesPrefab;
    public GameObject inputTypesPrefab;
    public GameObject emptyPrefab;
    public GameObject equalPrefab;
    public GameObject blankPrefab;
    public GameObject numberPrefab;
    public GameObject resultPrefab;
    public GameObject operationPrefab;
    private Transform equals;
    private Transform blanks;
    private Transform numbers;
    private Transform results;
    private Transform operations;
    private Transform gameArea;

    public void StartCrossSum()
    {
        gameArea = GetComponent<Transform>();
        CreateEmptyParents();
        CreateGridArea();
        CreateToggleRow();
        CreateInputTypes();
    }

    void CreateEmptyParents()
    {
        GameObject temp;
        float x = 0f;
        float y = 0f;
        int z = 2;

        temp = Instantiate(emptyPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Blanks";
        blanks = temp.transform;

        temp = Instantiate(emptyPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Equals";
        equals = temp.transform;

        temp = Instantiate(emptyPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Numbers";
        numbers = temp.transform;

        temp = Instantiate(emptyPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Operations";
        operations = temp.transform;

        temp = Instantiate(emptyPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Results";
        results = temp.transform;
    }

    void CreateGridArea()
    {
        int blanksCount = 0;
        int equalsCount = 0;
        int numbersCount = 0;
        int operationsCount = 0;
        int resultsCount = 0;

        float cellWidth = 1f;
        float cellHeight = 1f;
        
        float horizontalSpacing = 0f;
        float verticalSpacing = 0f;

        float x = 0f;
        float y = 0f;
        float z = 2f;

        for (int i = 0; i < 7; i++)
        {
            y = ((cellHeight + verticalSpacing) * (3f - i));
            for (int j = 0; j < 7; j++)
            {
                x = ((cellWidth + horizontalSpacing) * (j - 3f));
                if ((i % 2 == 0) && (j % 2 == 0))
                {
                    if ((i == 6) && (j == 6))
                    {
                        GameObject temp = Instantiate(blankPrefab, new Vector3(x,y,z), Quaternion.identity, blanks);
                        temp.name = blanksCount.ToString();
                        blanksCount++;
                    }
                    else if ((i == 6) || (j == 6))
                    {
                        GameObject temp = Instantiate(resultPrefab, new Vector3(x,y,z), Quaternion.identity, results);
                        temp.name = resultsCount.ToString();
                        resultsCount++;
                    }
                    else
                    {
                        GameObject temp = Instantiate(numberPrefab, new Vector3(x,y,z), Quaternion.identity, numbers);
                        temp.name = numbersCount.ToString();
                        numbersCount++;
                    }
                }
                else
                {
                    if (((i == 5) && (j == 6)) || ((i == 6) && (j == 5)))
                    {
                        GameObject temp = Instantiate(blankPrefab, new Vector3(x,y,z), Quaternion.identity, blanks);
                        temp.name = blanksCount.ToString();
                        blanksCount++;
                    }
                    else if (((i == 5) && (j % 2 == 0)) || ((i % 2 == 0) && (j == 5)))
                    {
                        GameObject temp = Instantiate(equalPrefab, new Vector3(x,y,z), Quaternion.identity, equals);
                        temp.name = equalsCount.ToString();
                        equalsCount++;
                    }
                    else if (((i < 5) && (j < 5)) && ((i % 2 == 0) || (j % 2 == 0)))
                    {
                        GameObject temp = Instantiate(operationPrefab, new Vector3(x,y,z), Quaternion.identity, operations);
                        temp.name = operationsCount.ToString();
                        operationsCount++;
                    }
                    else
                    {
                        GameObject temp = Instantiate(blankPrefab, new Vector3(x,y,z), Quaternion.identity, blanks);
                        temp.name = blanksCount.ToString();
                        blanksCount++;
                    }
                }
            }
        }
    }

    void CreateToggleRow()
    {
        float x = 0f;
        float y = -4.5f;
        int z = 2;
        GameObject temp = Instantiate(togglesPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Toggles";
    }

    void CreateInputTypes()
    {
        float x = 4.5f;
        float y = -2.5f;
        int z = 2;
        GameObject temp = Instantiate(inputTypesPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "InputTypes";
    }
}