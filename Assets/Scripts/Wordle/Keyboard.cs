using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Keyboard : MonoBehaviour
{

    private Button button;
    private Transform gameArea;
    private Transform guessArea;
    private Color defaultColor;

    void Start()
    {
        gameArea = transform.parent;
        guessArea = gameArea.GetChild(0);
        defaultColor = guessArea.GetComponent<GuessArea>().GetColor("default");
    }

    private int GetIndexFromLetter(string letter)
    {
        switch(letter)
        {
            case "Q": return 00;
            case "W": return 01;
            case "E": return 02;
            case "R": return 03;
            case "T": return 04;
            case "Y": return 05;
            case "U": return 06;
            case "I": return 07;
            case "O": return 08;
            case "P": return 09;
            case "A": return 10;
            case "S": return 11;
            case "D": return 12;
            case "F": return 13;
            case "G": return 14;
            case "H": return 15;
            case "J": return 16;
            case "K": return 17;
            case "L": return 18;
            case "Z": return 20;
            case "X": return 21;
            case "C": return 22;
            case "V": return 23;
            case "B": return 24;
            case "N": return 25;
            case "M": return 26;
            default: return 00;
        }
    }

    public void UpdateKeyboardLetterColor(string letter, Color color)
    {
        int index = GetIndexFromLetter(letter);
        transform.GetChild(index / 10).GetChild(index - ((index / 10) * 10)).GetComponent<KeyboardButton>().UpdateKeyboardLetterColor(color);
    }

    public void ResetKeyboardLetterColor()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.GetChild(0).GetChild(i).GetComponent<KeyboardButton>().UpdateKeyboardLetterColor(defaultColor);
        }

        for (int i = 0; i < 9; i++)
        {
            transform.GetChild(1).GetChild(i).GetComponent<KeyboardButton>().UpdateKeyboardLetterColor(defaultColor);
        }

        for (int i = 0; i < 7; i++)
        {
            transform.GetChild(2).GetChild(i).GetComponent<KeyboardButton>().UpdateKeyboardLetterColor(defaultColor);
        }
    }
}