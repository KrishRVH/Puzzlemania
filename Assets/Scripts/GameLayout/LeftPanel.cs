using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LeftPanel : MonoBehaviour
{
    private GameObject master;
    public GameObject buttonPrefab;
    private Transform leftPanel;
    float x = -14f;
    float y = 7f;
    float z = 2f;

    void Start()
    {
        master = GameObject.Find("Master");
        leftPanel = GetComponent<Transform>();
        if (master.GetComponent<GameState>().gameChoice == "Sudoku")
        {
            CreateSudokuUndoButton(0f);
        }

        if (master.GetComponent<GameState>().gameChoice == "CrossSum")
        {
            CreateCrossSumUndoButton(0f);
        }
    }

    void CreateSudokuUndoButton(float number)
    {
        GameObject temp = Instantiate(buttonPrefab, new Vector3(x,y - (number * 1.5f),z), Quaternion.identity, leftPanel);
        temp.name = "UndoButton";
        Button button = temp.GetComponent<Button>();
        TMP_Text buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Undo";
        temp.AddComponent<SudokuUndo>();
    }

    void CreateCrossSumUndoButton(float number)
    {
        GameObject temp = Instantiate(buttonPrefab, new Vector3(x,y - (number * 1.5f),z), Quaternion.identity, leftPanel);
        temp.name = "UndoButton";
        Button button = temp.GetComponent<Button>();
        TMP_Text buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Undo";
        temp.AddComponent<CrossSumUndo>();
    }


}