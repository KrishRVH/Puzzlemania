using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumOperation : MonoBehaviour
{

    private int index;
    private TMP_Text textBox;
    private Transform gameArea;

    void Start()
    {
        index = int.Parse(transform.name);
        textBox = transform.GetChild(0).GetComponent<TMP_Text>();
        gameArea = transform.parent.parent;
        GetValue();
    }

    private void GetValue()
    {
        textBox.text = gameArea.GetComponent<CrossSumPuzzle>().operations[index];
    }
}