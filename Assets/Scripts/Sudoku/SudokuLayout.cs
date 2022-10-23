using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SudokuLayout : MonoBehaviour
{

    public GameObject house;
    public GameObject toggles;
    public GameObject inputTypes;
    private Transform gameArea;

    void Start()
    {
        gameArea = GetComponent<Transform>();
    }

    public void StartSudoku()
    {
        gameArea = GetComponent<Transform>();
        CreateSudokuGrid();
        CreateToggleGrid();
        CreateInputTypes();
    }

    void CreateSudokuGrid()
    {
        int x = 0;
        int y = 0;
        int z = 2;
        int houseNumber = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                x = -3 + (j * 3);
                y = 3 - (i * 3);
                GameObject temp = Instantiate(house, new Vector3(x,y,z), Quaternion.identity, gameArea);
                switch(i.ToString() + j.ToString())
                {
                    case "00": houseNumber = 0; break;
                    case "01": houseNumber = 1; break;
                    case "02": houseNumber = 2; break;
                    case "10": houseNumber = 3; break;
                    case "11": houseNumber = 4; break;
                    case "12": houseNumber = 5; break;
                    case "20": houseNumber = 6; break;
                    case "21": houseNumber = 7; break;
                    case "22": houseNumber = 8; break;
                    default: break;
                }
                temp.name = "House" + houseNumber;
            }
        }
    }

    void CreateToggleGrid()
    {
        float x = 0.5f;
        float y = -5.25f;
        int z = 2;
        GameObject temp = Instantiate(toggles, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Toggles";
    }

    void CreateInputTypes()
    {
        float x = 5.5f;
        float y = -3.25f;
        int z = 2;
        GameObject temp = Instantiate(inputTypes, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "InputTypes";
    }
}