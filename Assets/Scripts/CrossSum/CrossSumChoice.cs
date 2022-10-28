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
        button.onClick.AddListener(OnCrossSumClick);
        master = GameObject.Find("Master");
    }
    
    void OnCrossSumClick()
    {
        master.GetComponent<GameState>().gameChoice = "CrossSum";
        SceneManager.LoadScene(sceneName);
    }
}