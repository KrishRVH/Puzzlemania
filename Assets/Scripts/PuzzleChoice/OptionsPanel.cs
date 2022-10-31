using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class OptionsPanel : MonoBehaviour
{
    public GameObject buttonPrefab;
    private GameObject master;
    private Transform optionsPanel;
    private TMP_Text buttonText;
    
    void Start()
    {
        optionsPanel = transform;
        master = GameObject.Find("Master");
        if (master.GetComponent<GameState>().gameChoice == "CrossSum")
        {
            CreateCrossSumOptions();
        }
        else if (master.GetComponent<GameState>().gameChoice == "Set")
        {
            CreateSetOptions();
        }
        else if (master.GetComponent<GameState>().gameChoice == "Sudoku")
        {
            CreateSudokuOptions();
        }
        else if (master.GetComponent<GameState>().gameChoice == "Wordle")
        {
            CreateWordleOptions();
        }
    }

    private void CreateCrossSumOptions()
    {
        GameObject temp;

        float x = 0f;
        float y = 3f;
        float z = 0f;

        temp = Instantiate(buttonPrefab, new Vector3(x,y,z), Quaternion.identity, optionsPanel);
        temp.name = "0";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Easy";

        temp = Instantiate(buttonPrefab, new Vector3(x,0f,z), Quaternion.identity, optionsPanel);
        temp.name = "1";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Medium";

        temp = Instantiate(buttonPrefab, new Vector3(x,-y,z), Quaternion.identity, optionsPanel);
        temp.name = "2";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Hard";
    }

    private void CreateSetOptions()
    {
        GameObject temp;

        float x = 0f;
        float y = 1.5f;
        float z = 0f;

        temp = Instantiate(buttonPrefab, new Vector3(x,y,z), Quaternion.identity, optionsPanel);
        temp.name = "0";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Time Attack";

        temp = Instantiate(buttonPrefab, new Vector3(x,-y,z), Quaternion.identity, optionsPanel);
        temp.name = "1";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Relaxed";
    }

    private void CreateSudokuOptions()
    {
        GameObject temp;

        float x = 0f;
        float y = 3f;
        float z = 0f;

        temp = Instantiate(buttonPrefab, new Vector3(x,y,z), Quaternion.identity, optionsPanel);
        temp.name = "0";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Easy";

        temp = Instantiate(buttonPrefab, new Vector3(x,0f,z), Quaternion.identity, optionsPanel);
        temp.name = "1";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Medium";

        temp = Instantiate(buttonPrefab, new Vector3(x,-y,z), Quaternion.identity, optionsPanel);
        temp.name = "2";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Hard";
    }

    private void CreateWordleOptions()
    {
        GameObject temp;

        float x = 3f;
        float xMultiplier = 3f;
        float y = 2f;
        float z = 0f;

        temp = Instantiate(buttonPrefab, new Vector3((-xMultiplier * x),y,z), Quaternion.identity, optionsPanel);
        temp.name = "0";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "3 Letter Word";

        temp = Instantiate(buttonPrefab, new Vector3(-x,y,z), Quaternion.identity, optionsPanel);
        temp.name = "1";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "4 Letter Word";

        temp = Instantiate(buttonPrefab, new Vector3(x,y,z), Quaternion.identity, optionsPanel);
        temp.name = "2";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "5 Letter Word";

        temp = Instantiate(buttonPrefab, new Vector3((xMultiplier * x),y,z), Quaternion.identity, optionsPanel);
        temp.name = "3";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "6 Letter Word";

        temp = Instantiate(buttonPrefab, new Vector3((-xMultiplier * x),-y,z), Quaternion.identity, optionsPanel);
        temp.name = "4";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "7 Letter Word";

        temp = Instantiate(buttonPrefab, new Vector3(-x,-y,z), Quaternion.identity, optionsPanel);
        temp.name = "5";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "8 Letter Word";

        temp = Instantiate(buttonPrefab, new Vector3(x,-y,z), Quaternion.identity, optionsPanel);
        temp.name = "6";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "9 Letter Word";

        temp = Instantiate(buttonPrefab, new Vector3((xMultiplier * x),-y,z), Quaternion.identity, optionsPanel);
        temp.name = "7";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "10 Letter Word";
    }
}