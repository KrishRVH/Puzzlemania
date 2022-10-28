using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumUndo : MonoBehaviour
{
    public class NumberChange
    {
        public int Index;
        public string SavedValue;

        public NumberChange(int Index, string SavedValue)
        {
            this.Index = Index;
            this.SavedValue = SavedValue;
        }
    }

    public class NoteChange
    {
        public int Index;
        public int Note;
        public string SavedValue;

        public NoteChange(int Index, int Note, string SavedValue)
        {
            this.Index = Index;
            this.Note = Note;
            this.SavedValue = SavedValue;
        }
    }

    private Button button;
    private List<NumberChange> numberChangeList;
    private List<NoteChange> noteChangeList;
    private List<bool> changeType; // True = Number; False = Note;
    private Transform gameArea;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UndoClick);
        numberChangeList = new List<NumberChange>();
        noteChangeList = new List<NoteChange>();
        changeType = new List<bool>();
        gameArea = GameObject.Find("GameArea").transform;
    }

    private void UndoClick()
    {
        if (changeType.Count == 0) { return; }

        if (changeType[(changeType.Count - 1)]) { UndoLastNumberChange(); }
        else { UndoLastNoteChange(); }
        changeType.RemoveAt(changeType.Count - 1);
    }

    private void UndoLastNumberChange()
    {
        Transform cellTemp = gameArea.GetChild(2).GetChild(numberChangeList[(numberChangeList.Count - 1)].Index).GetComponent<Transform>();
        cellTemp.GetComponent<CrossSumNumber>().UndoChange(numberChangeList[(numberChangeList.Count - 1)].SavedValue);
        RemoveLastNumberChange();
    }

    public void AddNumberChange(int index, string savedValue)
    {
        changeType.Add(true);
        NumberChange temp = new NumberChange(index, savedValue);
        numberChangeList.Add(temp);
    }

    private void RemoveLastNumberChange()
    {
        numberChangeList.RemoveAt(numberChangeList.Count - 1);
    }

    private void UndoLastNoteChange()
    {
        Transform cellTemp = gameArea.GetChild(2).GetChild(noteChangeList[(noteChangeList.Count - 1)].Index).GetComponent<Transform>();
        Transform noteTemp = cellTemp.GetChild(0).GetChild(noteChangeList[(noteChangeList.Count - 1)].Note);
        noteTemp.GetComponent<CrossSumNote>().UndoChange(noteChangeList[(noteChangeList.Count - 1)].SavedValue);
        RemoveLastNoteChange();
    }

    public void AddNoteChange(int index, int note, string savedValue)
    {
        changeType.Add(false);
        NoteChange temp = new NoteChange(index, note, savedValue);
        noteChangeList.Add(temp);
    }

    private void RemoveLastNoteChange()
    {
        noteChangeList.RemoveAt(noteChangeList.Count - 1);
    }
}