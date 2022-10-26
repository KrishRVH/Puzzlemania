using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordleLayout : MonoBehaviour
{

    public GameObject keyboard;
    public GameObject guessLetter;
    public GameObject guessAreaPrefab;
    public GameObject guessRowPrefab;
    private Transform gameArea;
    private Transform guessArea;
    private Transform guessRow;

    void Start()
    {
        gameArea = GetComponent<Transform>();
    }

    public void StartWordle(int wordLength, int maxGuesses)
    {
        gameArea = GetComponent<Transform>();
        CreateGuessGrid(wordLength, maxGuesses);
        CreateKeyboard();
    }

    void CreateKeyboard()
    {
        float x = 0f;
        float y = -5.5f;
        int z = 2;
        GameObject temp = Instantiate(keyboard, new Vector3(x,y,z), Quaternion.identity, gameArea);
        temp.name = "Keyboard";

        List<string> topRow = new List<string>();
        string[] topArray = {"Q","W","E","R","T","Y","U","I","O","P"};
        topRow.AddRange(topArray);
        for (int i = 0; i < topRow.Count; i++)
        {
            temp.transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = topRow[i];
        }

        List<string> middleRow = new List<string>();
        string[] middlerray = {"A","S","D","F","G","H","J","K","L"};
        middleRow.AddRange(middlerray);
        for (int i = 0; i < middleRow.Count; i++)
        {
            temp.transform.GetChild(1).GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = middleRow[i];
        }

        List<string> bottomRow = new List<string>();
        string[] bottomArray = {"Z","X","C","V","B","N","M"};
        bottomRow.AddRange(bottomArray);
        for (int i = 0; i < bottomRow.Count; i++)
        {
            temp.transform.GetChild(2).GetChild(i).GetChild(0).GetComponent<TMP_Text>().text = bottomRow[i];
        }
    }

    void CreateGuessGrid(int wordLength, int maxGuesses)
    {
        int z = 2;
        GameObject temp = Instantiate(guessAreaPrefab, new Vector3(0, 0, z), Quaternion.identity, gameArea);
        temp.name = "GuessArea";
        guessArea = temp.transform;

        for (int i = 0; i < maxGuesses; i++)
        {
            float y = (float)maxGuesses - (i * 1.25f);
            temp = Instantiate(guessRowPrefab, new Vector3(0, y, z), Quaternion.identity, guessArea);
            temp.name = i.ToString();
            guessRow = temp.transform;
            for (int j = 0; j < wordLength; j++)
            {
                float x = ((j * 1.1f) - ((wordLength - 1) * 0.55f));
                temp = Instantiate(guessLetter, new Vector3(x, y, z), Quaternion.identity, guessRow);
                temp.name = j.ToString();
            }
        }
    }
}