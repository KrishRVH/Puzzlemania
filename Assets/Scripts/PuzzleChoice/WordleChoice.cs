using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class WordleChoice : MonoBehaviour
{
    private Button button;
    private GameObject master;
    private GameObject optionsPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSudokuClick);
        master = GameObject.Find("Master");
    }
    
    void OnSudokuClick()
    {
        master.GetComponent<GameState>().gameChoice = "Wordle";
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "OptionsPanel")
            {
                optionsPanel = temp;
                break;
            }
        }
        optionsPanel.SetActive(true);
    }
}