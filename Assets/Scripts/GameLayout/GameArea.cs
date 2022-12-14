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
        if (master.GetComponent<GameState>().gameChoice == "CrossSum")
        {
            GetComponent<CrossSumPuzzle>().PlayCrossSum();
        }
        else if (master.GetComponent<GameState>().gameChoice == "Set")
        {
            GetComponent<SetPuzzle>().PlaySet();
        }
        else if (master.GetComponent<GameState>().gameChoice == "Sudoku")
        {
            GetComponent<SudokuPuzzle>().PlaySudoku();
        }
        else if (master.GetComponent<GameState>().gameChoice == "Wordle")
        {
            GetComponent<WordlePuzzle>().PlayWordle();
        }
    }
}