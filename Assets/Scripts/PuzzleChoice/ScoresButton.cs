using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoresButton : MonoBehaviour
{
    private GameObject master;
    private Button button;
    public string sceneName;
    private GameObject scoresPanel;

    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("Master");
        button = GetComponent<Button>();
        button.onClick.AddListener(ScoresClick);
    }

    void ScoresClick()
    {
        GameObject[] rootObjects = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (GameObject temp in rootObjects)
        {
            if (temp.name == "ScoresPanel")
            {
                scoresPanel = temp;
                break;
            }
        }
        scoresPanel.SetActive(true);
    }
}
