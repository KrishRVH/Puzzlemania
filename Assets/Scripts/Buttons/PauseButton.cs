using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{

    private Button button;
    private GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(PauseClick);
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "PausePanel")
            {
                pausePanel = temp;
                break;
            }
        }
    }

    void PauseClick()
    {
        pausePanel.SetActive(true);
    }
}
