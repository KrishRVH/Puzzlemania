using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class WordlePuzzle : MonoBehaviour
{
    public TextAsset wordList;
    public int wordLength;
    public string randWord;

    public void PlayWordle()
    {
        wordLength = 5;
        string[] words = Regex.Split( wordList.text, "\n|\r|\r\n" );
        List<string> filtered = new List<string>();
        foreach (string word in words)
        {
            if (word.Length == wordLength)
            {
                filtered.Add(word);
            }
        }
        randWord = filtered[(int)(Random.value * filtered.Count)];
        Debug.Log(randWord);
        transform.GetComponent<WordleLayout>().StartWordle(wordLength);
    }
}