using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RightPanel : MonoBehaviour
{
    public GameObject buttonPrefab;
    private Transform rightPanel;
    float x = 14f;
    float y = 7f;
    float z = 2f;

    void Start()
    {
        rightPanel = GetComponent<Transform>();
        CreatePauseButton(0f);
        CreateHintButton(1f);
        CreateHowToButton(2f);
        CreateQuitButton(3f);
    }

    void CreatePauseButton(float number)
    {
        GameObject temp = Instantiate(buttonPrefab, new Vector3(x,y - (number * 1.5f),z), Quaternion.identity, rightPanel);
        temp.name = "PauseButton";
        Button button = temp.GetComponent<Button>();
        TMP_Text buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Pause";
        temp.AddComponent<PauseButton>();
    }

    void CreateHintButton(float number)
    {
        GameObject temp = Instantiate(buttonPrefab, new Vector3(x,y - (number * 1.5f),z), Quaternion.identity, rightPanel);
        temp.name = "HintButton";
        Button button = temp.GetComponent<Button>();
        TMP_Text buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Hint";
        temp.AddComponent<HintButton>();
    }

    void CreateHowToButton(float number)
    {
        GameObject temp = Instantiate(buttonPrefab, new Vector3(x,y - (number * 1.5f),z), Quaternion.identity, rightPanel);
        temp.name = "HowToButton";
        Button button = temp.GetComponent<Button>();
        TMP_Text buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "How To Play";
        temp.AddComponent<HowToButton>();
    }

    void CreateQuitButton(float number)
    {
        GameObject temp = Instantiate(buttonPrefab, new Vector3(x,y - (number * 1.5f),z), Quaternion.identity, rightPanel);
        temp.name = "QuitButton";
        Button button = temp.GetComponent<Button>();
        TMP_Text buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Quit";
        temp.AddComponent<QuitButton>();
    }
}