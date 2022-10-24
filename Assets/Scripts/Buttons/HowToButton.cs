using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HowToButton : MonoBehaviour
{

    private Button button;
    private GameObject howToPanel;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(HowToClick);
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "HowToPanel")
            {
                howToPanel = temp;
                break;
            }
        }
    }

    void HowToClick()
    {
        howToPanel.SetActive(true);
    }
}