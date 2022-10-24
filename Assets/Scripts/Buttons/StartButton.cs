using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    private Button button;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartClick);
        sceneName = "PuzzleChoice";
    }

    void StartClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}