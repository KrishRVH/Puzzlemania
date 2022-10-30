using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GuessArea : MonoBehaviour
{
    private Transform gameArea;
    private Transform keyboard;
    public int guessRow;
    public int guessIndex;
    private int wordLength;
    private int maxGuesses;
    private string[] words;
    private string randWord;
    private string submittedWord;
    private Color defaultColor;
    private Color correctColor;
    private Color incorrectColor;
    private Color partialColor;
    private Color selectedColor;
    private GameObject winPanel;
    private GameObject losePanel;

    void Start()
    {
        gameArea = transform.parent;
        keyboard = gameArea.GetChild(1);
        guessRow = 0;
        guessIndex = 0;
        defaultColor = Color.white;
        correctColor = Color.green;
        incorrectColor = Color.gray;
        partialColor = Color.yellow;
        selectedColor = new Color(0.7f,0.7f,0.7f,1f);
    }

    public Color GetColor(string color)
    {
        if (color == "default") { return defaultColor; }
        else if (color == "correct") { return correctColor; }
        else if (color == "incorrect") { return incorrectColor; }
        else if (color == "partial") { return partialColor; }
        else if (color == "selected") { return selectedColor; }
        return defaultColor;
    }

    public void UpdateGuessRow(int row)
    {
        maxGuesses = gameArea.GetComponent<WordlePuzzle>().maxGuesses;
        if (row >= maxGuesses)
        {
            // Lose
            for (int i = 0; i < wordLength; i++)
            {
                UpdateGuessLetterColor(guessRow, i, correctColor);
            }
            ResetKeyboardLetterColor();
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject temp in rootObjects)
            {
                if (temp.name == "LosePanel")
                {
                    losePanel = temp;
                    break;
                }
            }
            losePanel.SetActive(true);
            return;
        }
        else
        {
            guessRow = row;
        }
    }

    public void UpdateGuessIndex(int index)
    {

        if (index == guessIndex) { return; }
        UpdateGuessLetterColor(guessRow, guessIndex, defaultColor);

        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        if (index >= wordLength)
        {
            //UpdateGuessRow(guessRow + 1);
        }
        else if (index < 0)
        {
            //UpdateGuessRow(guessRow - 1);
        }
        else
        {
            guessIndex = index;
        }
        UpdateGuessLetterColor(guessRow, guessIndex, selectedColor);
    }

    public int GetCurrentGuessRow() { return guessRow; }

    public int GetCurrentGuessIndex() { return guessIndex; }

    private void GetCurrentWord() { randWord = gameArea.GetComponent<WordlePuzzle>().randWord; }

    public void SubmitGuess()
    {
        submittedWord = "";
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        for (int i = 0; i < wordLength; i++)
        {
            submittedWord += transform.GetChild(guessRow).GetChild(i).GetComponent<GuessLetter>().GetCurrentLetter();
        }

        if (submittedWord.Length != wordLength) { return;}

        GetCurrentWord();
        bool validWord = false;
        words = gameArea.GetComponent<WordlePuzzle>().words;

        foreach (string word in words)
        {
            if (submittedWord == word)
            {
                validWord = true;
                break;
            }
        }

        if (!validWord) {  return; }

        UpdateGuessIndex(0);

        if (submittedWord == randWord)
        {
            // Win
            for (int i = 0; i < wordLength; i++)
            {
                UpdateGuessLetterColor(guessRow, i, correctColor);
            }
            ResetKeyboardLetterColor();
            GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (GameObject temp in rootObjects)
            {
                if (temp.name == "WinPanel")
                {
                    winPanel = temp;
                    break;
                }
            }
            winPanel.SetActive(true);
            return;
        }
        CheckForCorrectLetters();
        CheckForPartialLetters();
        CheckForIncorrectLetters();
        UpdateGuessRow((guessRow + 1));
        UpdateGuessLetterColor(guessRow, guessIndex, selectedColor);
    }

    private void CheckForCorrectLetters()
    {
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        for (int i = 0; i < wordLength; i++)
        {
            if (submittedWord[i] == randWord[i])
            {
                UpdateKeyboardLetterColor(submittedWord[i].ToString(), correctColor);
                UpdateGuessLetterColor(guessRow, i, correctColor);
                submittedWord = submittedWord.Remove(i,1);
                submittedWord = submittedWord.Insert(i,"-");
                randWord = randWord.Remove(i,1);
                randWord = randWord.Insert(i,"-");
            }
        }
    }

    private void CheckForPartialLetters()
    {
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        for (int i = 0; i < wordLength; i++)
        {
            if (submittedWord[i].ToString() == "-") { continue; }

            for (int j = 0; j < wordLength; j++)
            {
                if (randWord[j].ToString() == "-") { continue; }

                if (submittedWord[i] == randWord[j])
                {
                    UpdateKeyboardLetterColor(submittedWord[i].ToString(), partialColor);
                    UpdateGuessLetterColor(guessRow, i, partialColor);
                    submittedWord = submittedWord.Remove(i,1);
                    submittedWord = submittedWord.Insert(i,"-");
                    randWord = randWord.Remove(j,1);
                    randWord = randWord.Insert(j,"-");
                    break;
                }
            }
        }
    }

    private void CheckForIncorrectLetters()
    {
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        for (int i = 0; i < wordLength; i++)
        {
            if (submittedWord[i].ToString() != "-")
            {
                UpdateKeyboardLetterColor(submittedWord[i].ToString(), incorrectColor);
                UpdateGuessLetterColor(guessRow, i, incorrectColor);
            }
        }
    }

    private void UpdateKeyboardLetterColor(string letter, Color color)
    {
        keyboard.GetComponent<Keyboard>().UpdateKeyboardLetterColor(letter, color);
    }

    private void ResetKeyboardLetterColor()
    {
        keyboard.GetComponent<Keyboard>().ResetKeyboardLetterColor();
    }

    private void UpdateGuessLetterColor(int row, int index, Color color)
    {
        transform.GetChild(row).GetChild(index).GetComponent<GuessLetter>().UpdateGuessLetterColor(color);
    }

    public void ResetGuessRowColor(int row)
    {
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        for (int i = 0; i < wordLength; i++)
        {
            transform.GetChild(row).GetChild(i).GetComponent<GuessLetter>().UpdateGuessLetterColor(defaultColor);
        }
    }

    public void ResetGuessLetterColor()
    {
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        maxGuesses = gameArea.GetComponent<WordlePuzzle>().maxGuesses;
        for (int i = 0; i < maxGuesses; i++)
        {
            for (int j = 0; j < wordLength; j++)
            {
                transform.GetChild(i).GetChild(j).GetComponent<GuessLetter>().UpdateGuessLetterColor(defaultColor);
            }
        }
    }
}