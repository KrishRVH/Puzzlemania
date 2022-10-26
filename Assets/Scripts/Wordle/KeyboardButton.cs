using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyboardButton : MonoBehaviour
{

    private Button button;
    private Image image;
    private Transform gameArea;
    private Transform guessArea;
    private int wordLength;
    private int guessRow;
    private int guessIndex;
    private Color defaultColor;
    private Color correctColor;
    private Color incorrectColor;
    private Color partialColor;

    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(KeyboardClick);
        image = transform.GetComponent<Image>();
        gameArea = transform.parent.parent.parent;
        guessArea = gameArea.GetChild(0);
        defaultColor = guessArea.GetComponent<GuessArea>().GetColor("default");
        correctColor = guessArea.GetComponent<GuessArea>().GetColor("correct");
        incorrectColor = guessArea.GetComponent<GuessArea>().GetColor("incorrect");
        partialColor = guessArea.GetComponent<GuessArea>().GetColor("partial");
    }

    void KeyboardClick()
    {
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        guessRow = guessArea.GetComponent<GuessArea>().GetCurrentGuessRow();
        guessIndex = guessArea.GetComponent<GuessArea>().GetCurrentGuessIndex();
        guessArea.GetChild(guessRow).GetChild(guessIndex).GetComponent<GuessLetter>().UpdateLetter(transform.name);
        guessArea.GetComponent<GuessArea>().UpdateGuessIndex(guessIndex + 1);
    }

    public void UpdateKeyboardLetterColor(Color color)
    {
        if (image.color == correctColor)
        {
            return;
        }
        else if ((color == incorrectColor) && (image.color != defaultColor))
        {
            return;
        }
        else
        {
            image.color = color;
        }
    }
}