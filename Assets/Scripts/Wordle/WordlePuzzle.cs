using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class WordlePuzzle : MonoBehaviour
{
    private GameObject master;
    public TextAsset wordList;
    public string[] words;
    public int wordLength;
    public int maxGuesses;
    public string randWord;

    public void PlayWordle()
    {
        master = GameObject.Find("Master");
        wordLength = (int.Parse(master.transform.GetComponent<GameState>().gameOption) + 3);
        maxGuesses = CalculateMaxGuesses(wordLength);
        words = Regex.Split( wordList.text, "\n|\r|\r\n" );
        List<string> filtered = new List<string>();
        foreach (string word in words)
        {
            if (word.Length == wordLength)
            {
                filtered.Add(word);
            }
        }
        randWord = filtered[(int)(Random.value * filtered.Count)];
        transform.GetComponent<WordleLayout>().StartWordle(wordLength, maxGuesses);
    }

    private int CalculateMaxGuesses(int wordLength)
    {
        switch(wordLength)
        {
            case 3: return 6;
            case 4: return 6;
            case 5: return 6;
            case 6: return 6;
            case 7: return 7;
            case 8: return 7;
            case 9: return 8;
            case 10: return 8;
            default: return 6;
        }
    }
}