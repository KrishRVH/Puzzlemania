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
        maxGuesses = 6;
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
}