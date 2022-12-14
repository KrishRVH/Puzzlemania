using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OkayButton : MonoBehaviour
{
    private GameObject master;
    private Button button;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        master = GameObject.Find("Master");
        button = GetComponent<Button>();
        button.onClick.AddListener(OkayClick);
    }

    void OkayClick()
    {
        transform.parent.gameObject.SetActive(false);
        if (sceneName != "")
        {
            if (sceneName != "GameLayout")
            {
                master.transform.GetComponent<GameState>().gameChoice = "";
            }
            SceneManager.LoadScene(sceneName);
        }
    }
}
