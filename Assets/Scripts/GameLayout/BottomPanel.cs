using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BottomPanel : MonoBehaviour
{
    private GameObject master;
    public GameObject setTextBox;
    private GameObject timeIsUpPanel;
    private Transform gameArea;
    private Transform bottomPanel;
    private TMP_Text setTimerTextBox;
    private TMP_Text setsAvailableTextBox;
    private TMP_Text setsFoundTextBox;
    private float timeRemaining;
    private bool timeAttack = false;

    void Start()
    {
        master = GameObject.Find("Master");
        bottomPanel = GetComponent<Transform>();
        gameArea = GameObject.Find("GameArea").transform;

        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "TimeIsUpPanel")
            {
                timeIsUpPanel = temp;
                break;
            }
        }

        if (master.GetComponent<GameState>().gameChoice == "CrossSum")
        {

        }
        else if (master.GetComponent<GameState>().gameChoice == "Set")
        {
            string option = master.transform.GetComponent<GameState>().gameOption;
            if (option == "0")
            {
                timeAttack = true;
                timeRemaining = 61f;
                SetTimeAttack();
            }
            else if (option == "1")
            {
                SetRelaxed();
                //setsAvailableTextBox = transform.GetChild(0).GetComponent<TMP_Text>();
            }
        }
        else if (master.GetComponent<GameState>().gameChoice == "Sudoku")
        {

        }
        else if (master.GetComponent<GameState>().gameChoice == "Wordle")
        {

        }
    }

    void Update()
    {
        if (timeAttack)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                setTimerTextBox.text = ("Time Remaining: " + (int)(timeRemaining));
            }
            else
            {
                timeAttack = false;
                timeIsUpPanel.SetActive(true);
            }
        }
    }

    private void SetTimeAttack()
    {
        CreateSetsFoundTextBox(-10f);
        CreateSetTimerTextBox(0f);
        CreateSetsAvailableTextBox(10f);
    }

    private void SetRelaxed()
    {
        CreateSetsFoundTextBox(-5f);
        CreateSetsAvailableTextBox(5f);
    }

    private void CreateSetTimerTextBox(float x)
    {
        float y = transform.position.y;
        float z = 2f;
        GameObject temp = Instantiate(setTextBox, new Vector3(x,y,z), Quaternion.identity, bottomPanel);
        temp.name = "SetTimerTextBox";
        setTimerTextBox = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        setTimerTextBox.text = ("Time Remaining: " + (int)(timeRemaining));
    }

    private void CreateSetsFoundTextBox(float x)
    {
        float y = transform.position.y;
        float z = 2f;
        GameObject temp = Instantiate(setTextBox, new Vector3(x,y,z), Quaternion.identity, bottomPanel);
        temp.name = "SetsFoundTextBox";
        setsFoundTextBox = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        setsFoundTextBox.text = ("Sets Found: 0");
    }

    private void CreateSetsAvailableTextBox(float x)
    {
        float y = transform.position.y;
        float z = 2f;
        GameObject temp = Instantiate(setTextBox, new Vector3(x,y,z), Quaternion.identity, bottomPanel);
        temp.name = "SetsAvailableTextBox";
        setsAvailableTextBox = temp.transform.GetChild(0).GetComponent<TMP_Text>();
    }

    public void UpdateSetsAvailableTextBox(int availableSets)
    {
        setsAvailableTextBox.text = ("Sets Available: " + availableSets);
    }

    public void UpdateSetsFoundTextBox(int setsFound)
    {
        setsFoundTextBox.text = ("Sets Found: " + setsFound);
    }
}