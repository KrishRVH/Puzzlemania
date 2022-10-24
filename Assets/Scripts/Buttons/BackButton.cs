using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{

    private Button button;
    public string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(BackClick);
    }

    void BackClick()
    {
        if (transform.parent.name.Substring(transform.parent.name.Length - 5) == "Panel")
        {
            transform.parent.gameObject.SetActive(false);
        }
        if (sceneName != "")
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
