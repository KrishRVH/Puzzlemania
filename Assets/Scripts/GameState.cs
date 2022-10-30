using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public string gameChoice;
    public string gameOption;

    void Start()
    {
        gameChoice = "";
        gameOption = "";
    }
}