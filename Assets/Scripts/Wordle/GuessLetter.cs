using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuessLetter : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;
    private Image image;
    private Transform gameArea;
    private Transform guessArea;
    private int wordLength;
    private int guessRow;
    private int guessIndex;
    private Color selectedColor;

    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(GuessLetterClick);
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
        image = GetComponent<Image>();
        gameArea = transform.parent.parent.parent;
        guessArea = transform.parent.parent;
        guessRow = int.Parse(transform.parent.name);
        guessIndex = int.Parse(transform.name);
        selectedColor = guessArea.GetComponent<GuessArea>().GetColor("selected");
        if (guessRow == 0 && guessIndex == 0)
        {
            image.color = selectedColor;
        }
    }

    private void GuessLetterClick()
    {
        if (guessRow == guessArea.GetComponent<GuessArea>().GetCurrentGuessRow())
        {
            guessArea.GetComponent<GuessArea>().UpdateGuessIndex(guessIndex);
        }
    }

    public void UpdateGuessLetterColor(Color color)
    {
        image.color = color;
    }

    public string GetCurrentLetter()
    {
        return buttonText.text;
    }

    public void UpdateLetter(string letter)
    {
        buttonText.text = letter;
    }
}