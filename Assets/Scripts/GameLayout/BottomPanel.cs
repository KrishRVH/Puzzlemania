using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BottomPanel : MonoBehaviour
{
    private GameObject master;
    public GameObject setTextBox;
    private Transform gameArea;
    private Transform bottomPanel;
    private TMP_Text textBox;

    void Start()
    {
        master = GameObject.Find("Master");
        bottomPanel = GetComponent<Transform>();
        gameArea = GameObject.Find("GameArea").transform;
        if (master.GetComponent<GameState>().gameChoice == "Set")
        {
            CreateSetTextBox();
            textBox = transform.GetChild(0).GetComponent<TMP_Text>();
        }
    }

    private void CreateSetTextBox()
    {
        float x = 0f;
        float y = transform.position.y;
        float z = 2f;
        GameObject temp = Instantiate(setTextBox, new Vector3(x,y,z), Quaternion.identity, bottomPanel);
        temp.name = "TextBox";
    }

    public void UpdateTextBox(int availableSets)
    {
        textBox.text = ("Sets Available: " + availableSets);
    }

}