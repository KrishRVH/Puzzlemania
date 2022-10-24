using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameArea : MonoBehaviour
{
    private GameObject master;

    void Start()
    {
        master = GameObject.Find("Master");
        if (master.GetComponent<GameState>().gameChoice == "Sudoku")
        {
            GetComponent<SudokuPuzzle>().PlaySudoku();
        }
    }
}