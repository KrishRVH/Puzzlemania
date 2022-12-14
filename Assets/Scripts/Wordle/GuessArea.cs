using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private Regex lettersOnly;
    private EventSystem eventSystem;

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

        string regexPatternLetters = @"^[a-zA-Z]$";
        lettersOnly = new Regex(regexPatternLetters);

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "EventSystem")
            {
                eventSystem = temp.transform.GetComponent<EventSystem>();
                break;
            }
        }
        wordLength = gameArea.GetComponent<WordlePuzzle>().wordLength;
        TrackGame(wordLength);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            if ((Input.GetKeyDown(KeyCode.KeypadEnter)) || (Input.GetKeyDown(KeyCode.Return)))
            {
                SubmitGuess();
            }
            else if (Input.GetKeyDown(KeyCode.Backspace))
            {
                transform.parent.GetChild(1).GetChild(3).GetComponent<BackspaceButton>().BackspaceClick();
            }
            else if (IsInputLetter())
            {
                transform.GetChild(guessRow).GetChild(guessIndex).GetComponent<GuessLetter>().UpdateLetter(Input.inputString.ToUpper());
                UpdateGuessIndex((guessIndex + 1));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                UpdateGuessIndex((guessIndex + 1));
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                UpdateGuessIndex((guessIndex - 1));
            }
        }
    }

    private bool IsInputLetter()
    {
        if ((lettersOnly.IsMatch(Input.inputString)) && (Input.inputString.Length == 1))
        {
            return true;
        }
        return false;
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
        if (index >= wordLength) {}
        else if (index < 0) {}
        else
        {
            guessIndex = index;
        }
        UpdateGuessLetterColor(guessRow, guessIndex, selectedColor);
        eventSystem.SetSelectedGameObject(transform.GetChild(guessRow).GetChild(guessIndex).gameObject);
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
            TrackWin(wordLength, (guessRow + 1));
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

    private void TrackGame(int wordLength)
    {
        StartCoroutine(WaitTenSeconds());
        string tempString = "WordleGamesPlayed" + wordLength.ToString();
        int temp = PlayerPrefs.GetInt(tempString, 0);
        PlayerPrefs.SetInt(tempString, (temp + 1));
        PlayerPrefs.Save();
    }

    IEnumerator WaitTenSeconds()
    {
        yield return new WaitForSecondsRealtime(10);
    }

    private void TrackWin(int wordLength, int guesses)
    {
        string tempString = "WordleGamesWon" + wordLength.ToString();
        int temp = PlayerPrefs.GetInt(tempString, 0);
        PlayerPrefs.SetInt(tempString, (temp + 1));
        TrackGuessCount(wordLength, guesses);
    }
    
    private void TrackGuessCount(int wordLength, int guesses)
    {
        string tempString = "WordleGuessCount" + wordLength.ToString() + "-" + guesses.ToString();
        int temp = PlayerPrefs.GetInt(tempString, 0);
        PlayerPrefs.SetInt(tempString, (temp + 1));
        PlayerPrefs.Save();
    }
}