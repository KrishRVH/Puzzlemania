using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HelpButton : MonoBehaviour
{

    private Button button;
    private GameObject helpPanel;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HelpClick);
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "HelpPanel")
            {
                helpPanel = temp;
                break;
            }
        }
    }

    void HelpClick()
    {
        helpPanel.SetActive(true);
    }
}