using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BackspaceButton : MonoBehaviour
{

    private Button button;
    private Transform gameArea;
    private Transform guessArea;
    private int wordLength;
    private int guessRow;
    private int guessIndex;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(BackspaceClick);
        gameArea = transform.parent.parent;
        guessArea = gameArea.GetChild(0);
    }

    public void BackspaceClick()
    {
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        guessRow = guessArea.GetComponent<GuessArea>().GetCurrentGuessRow();
        guessIndex = guessArea.GetComponent<GuessArea>().GetCurrentGuessIndex();
        if (guessArea.GetChild(guessRow).GetChild(guessIndex).GetComponent<GuessLetter>().GetCurrentLetter() != "")
        {
            guessArea.GetChild(guessRow).GetChild(guessIndex).GetComponent<GuessLetter>().UpdateLetter("");
        }
        else if (guessIndex != 0)
        {
            guessArea.GetComponent<GuessArea>().UpdateGuessIndex(guessIndex - 1);
            guessArea.GetChild(guessRow).GetChild(guessIndex - 1).GetComponent<GuessLetter>().UpdateLetter("");
        }
    }
}