using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OkayButton : MonoBehaviour
{

    private Button button;
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OkayClick);
        sceneName = "PuzzleChoice";
    }

    void OkayClick()
    {
        transform.parent.gameObject.SetActive(false);
        SceneManager.LoadScene(sceneName);
    }
}
