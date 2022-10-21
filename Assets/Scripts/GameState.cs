using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public string gameName;

    public class HighScore : MonoBehaviour
    {
        public int sudoku;
        public int set;
        public int crossSum;
        public int wordle;
    }

    public class BestTime : MonoBehaviour
    {
        public int sudoku;
        public int set;
        public int crossSum;
        public int wordle;
    }
}