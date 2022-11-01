using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsButton : MonoBehaviour
{
    private Button button;
    private GameObject settingsPanel;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SettingsClick);
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "SettingsPanel")
            {
                settingsPanel = temp;
                break;
            }
        }
    }

    void SettingsClick()
    {
        settingsPanel.SetActive(true);
    }
}