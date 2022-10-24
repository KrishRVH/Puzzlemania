using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumChoice : MonoBehaviour
{
    private Button button;
    private string sceneName = "GameLayout";
    private GameObject master;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSudokuClick);
        master = GameObject.Find("Master");
    }
    
    void OnSudokuClick()
    {
        master.GetComponent<GameState>().gameChoice = "CrossSum";
        SceneManager.LoadScene(sceneName);
    }
}