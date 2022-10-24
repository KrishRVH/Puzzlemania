using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SudokuUndo : MonoBehaviour
{
    public class NumberChange
    {
        public int House;
        public int Index;
        public string SavedValue;

        public NumberChange(int House, int Index, string SavedValue)
        {
            this.House = House;
            this.Index = Index;
            this.SavedValue = SavedValue;
        }
    }

    public class NoteChange
    {
        public int House;
        public int Index;
        public int Note;
        public string SavedValue;

        public NoteChange(int House, int Index, int Note, string SavedValue)
        {
            this.House = House;
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
        Transform houseTemp = gameArea.GetChild(numberChangeList[(numberChangeList.Count - 1)].House).GetComponent<Transform>();
        Transform cellTemp = houseTemp.GetChild(numberChangeList[(numberChangeList.Count - 1)].Index).GetComponent<Transform>();
        cellTemp.GetComponent<SudokuCell>().UndoChange(numberChangeList[(numberChangeList.Count - 1)].SavedValue);
        RemoveLastNumberChange();
    }

    public void AddNumberChange(int house, int index, string savedValue)
    {
        changeType.Add(true);
        NumberChange temp = new NumberChange(house, index, savedValue);
        numberChangeList.Add(temp);
    }

    private void RemoveLastNumberChange()
    {
        numberChangeList.RemoveAt(numberChangeList.Count - 1);
    }

    private void UndoLastNoteChange()
    {
        Transform houseTemp = gameArea.GetChild(noteChangeList[(noteChangeList.Count - 1)].House).GetComponent<Transform>();
        Transform cellTemp = houseTemp.GetChild(noteChangeList[(noteChangeList.Count - 1)].Index).GetComponent<Transform>();
        Transform noteTemp = cellTemp.GetChild(0).GetChild(noteChangeList[(noteChangeList.Count - 1)].Note);
        noteTemp.GetComponent<SudokuNote>().UndoChange(noteChangeList[(noteChangeList.Count - 1)].SavedValue);
        RemoveLastNoteChange();
    }

    public void AddNoteChange(int house, int index, int note, string savedValue)
    {
        changeType.Add(false);
        NoteChange temp = new NoteChange(house, index, note, savedValue);
        noteChangeList.Add(temp);
    }

    private void RemoveLastNoteChange()
    {
        noteChangeList.RemoveAt(noteChangeList.Count - 1);
    }
}