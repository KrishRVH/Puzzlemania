using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WordleLayout : MonoBehaviour
{

    public GameObject keyboard;
    public GameObject guessLetter;
    private Transform gameArea;
    private int maxGuess = 6;

    void Start()
    {
        gameArea = GetComponent<Transform>();
        maxGuess = 6;
    }

    public void StartWordle(int wordLength)
    {
        gameArea = GetComponent<Transform>();
        CreateGuessGrid(wordLength);
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

    void CreateGuessGrid(int wordLength)
    {
        int z = 2;
        for (int i = 0; i < maxGuess; i++)
        {
            float y = 5f - (i * 1.25f);
            for (int j = 0; j < wordLength; j++)
            {
                float x = ((j * 1.1f) - ((wordLength * 0.55f) - 0.55f));
                GameObject temp = Instantiate(guessLetter, new Vector3(x,y,z), Quaternion.identity, gameArea);
                temp.name = j.ToString();
            }
        }
    }
}