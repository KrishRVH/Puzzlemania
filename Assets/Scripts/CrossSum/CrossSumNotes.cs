using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumNotes : MonoBehaviour
{
    public void EnableNotes(bool enable)
    {
        Transform note;
        if (enable)
        {
            for (int i = 0; i < 9; i++)
            {
                note = transform.GetChild(i);
                note.GetChild(0).GetComponent<TMP_Text>().color = Color.black;
            }
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                note = transform.GetChild(i);
                note.GetChild(0).GetComponent<TMP_Text>().color = Color.white;
            }
        }
    }

    public void EnableNote(int value)
    {
        Transform note;
        note = transform.GetChild(value - 1);
        if (note.GetComponent<CrossSumNote>().GetCurrentValue() == value.ToString())
        {
            note.GetComponent<CrossSumNote>().EnableNote(false);
        }
        else
        {
            note.GetComponent<CrossSumNote>().EnableNote(true);
        }
    }

    public void ClearNotes()
    {
        Transform note;
        for (int i = 0; i < 9; i++)
        {
            note = transform.GetChild(i);
            note.GetComponent<CrossSumNote>().EnableNote(false);
        }
    }
}