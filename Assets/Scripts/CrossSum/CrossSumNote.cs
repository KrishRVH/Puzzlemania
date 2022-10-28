using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumNote : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;
    private string noteValue;
    private Transform gameArea;
    private Transform number;
    public int index;
    private Transform notes;
    private Transform toggles;
    private Transform inputTypes;
    private Transform leftPanel;

    void Start()
    {
        noteValue = transform.name;
        gameArea = transform.parent.parent.parent.parent;
        notes = transform.parent;
        number = transform.parent.parent;
        index = number.GetComponent<CrossSumNumber>().index;
        toggles = gameArea.GetChild(5);
        inputTypes = gameArea.GetChild(6);
        button = GetComponent<Button>();
        buttonText = transform.GetChild(0).GetComponent<TMP_Text>();
        leftPanel = GameObject.Find("LeftPanel").transform;
    }

    public string GetCurrentValue()
    {
        return buttonText.text;
    }

    public void EnableNote(bool enable)
    {
        if (enable)
        {
            if (buttonText.text != noteValue)
            {
                TrackChange();
                buttonText.text = noteValue;
            }
        }
        else
        {
            if (buttonText.text != "")
            {
                TrackChange();
                buttonText.text = "";
            }
        }
    }

    private void TrackChange()
    {
        Transform undoButton = leftPanel.GetChild(0).GetComponent<Transform>();
        undoButton.GetComponent<CrossSumUndo>().AddNoteChange(index, (int.Parse(noteValue) - 1), buttonText.text);
    }

    public void UndoChange(string savedValue)
    {
        buttonText.text = savedValue;
    }
}