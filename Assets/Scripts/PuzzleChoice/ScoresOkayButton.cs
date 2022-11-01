using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresOkayButton : MonoBehaviour
{
    private string sceneName = "PuzzleChoice";
    private GameObject master;
    private Transform optionsPanel;
    private Button button;

    void Start()
    {
        master = GameObject.Find("Master");
        optionsPanel = transform.parent;
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(OptionClick);
    }

    private void OptionClick()
    {
        master.GetComponent<GameState>().gameOption = transform.name;
        optionsPanel.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}