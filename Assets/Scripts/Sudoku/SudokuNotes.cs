using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuNotes : MonoBehaviour
{
    public void EnableNotes(bool enable)
    {
        Transform note;
        Color white = new Color(1f,1f,1f,1f);
        Color black = new Color(0f,0f,0f,1f);
        if (enable)
        {
            for (int i = 0; i < 9; i++)
            {
                note = transform.GetChild(i);
                note.GetChild(0).GetComponent<TMP_Text>().color = black;
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                note = transform.GetChild(i);
                note.GetChild(0).GetComponent<TMP_Text>().color = white;
            }
        }
    }

    public void EnableNote(int value)
    {
        Transform note;
        note = transform.GetChild(value - 1);
        if (note.GetComponent<SudokuNote>().GetCurrentValue() == value.ToString())
        {
            note.GetComponent<SudokuNote>().EnableNote(false);
        }
        else
        {
            note.GetComponent<SudokuNote>().EnableNote(true);
        }
    }

    public void ClearNotes()
    {
        Transform note;
        for (int i = 0; i < 9; i++)
        {
            note = transform.GetChild(i);
            note.GetComponent<SudokuNote>().EnableNote(false);
        }
    }
}