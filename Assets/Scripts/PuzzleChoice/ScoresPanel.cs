using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoresPanel : MonoBehaviour
{
    public GameObject buttonPrefab;
    private GameObject master;
    private Transform scoresPanel;
    private TMP_Text buttonText;
    private Transform crossSumScores;
    private Transform setScores;
    private Transform sudokuScores;
    private Transform wordleScores;
    
    void Start()
    {
        scoresPanel = transform;
        master = GameObject.Find("Master");
        crossSumScores = transform.GetChild(1);
        setScores = transform.GetChild(2);
        sudokuScores = transform.GetChild(3);
        wordleScores = transform.GetChild(4);
        GatherScores();
        CreateBackButton();
    }

    private void CreateBackButton()
    {
        GameObject temp;

        float x = 0f;
        float y = -8f;
        float z = 0f;

        temp = Instantiate(buttonPrefab, new Vector3(x,y,z), Quaternion.identity, scoresPanel);
        temp.name = "BackButton";
        buttonText = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        buttonText.text = "Back";
    }

    private void GatherScores()
    {
        TMP_Text tempText;
        tempText = crossSumScores.GetChild(0).GetComponent<TMP_Text>();
        tempText.text = PlayerPrefs.GetInt("CrossSumGamesPlayed",0).ToString();

        tempText = crossSumScores.GetChild(1).GetComponent<TMP_Text>();
        tempText.text = PlayerPrefs.GetInt("CrossSumGamesWon",0).ToString();

        tempText = setScores.GetChild(0).GetComponent<TMP_Text>();
        tempText.text = PlayerPrefs.GetInt("RelaxedTotalSets",0).ToString();

        tempText = setScores.GetChild(1).GetComponent<TMP_Text>();
        tempText.text = PlayerPrefs.GetInt("TimeAttackHighestSets",0).ToString();

        tempText = sudokuScores.GetChild(0).GetComponent<TMP_Text>();
        tempText.text = PlayerPrefs.GetInt("SudokuGamesPlayed",0).ToString();

        tempText = sudokuScores.GetChild(1).GetComponent<TMP_Text>();
        tempText.text = PlayerPrefs.GetInt("SudokuGamesWon",0).ToString();

        int count = 3;
        for (int i = 0; i < 16; i += 2)
        {
            tempText = wordleScores.GetChild(i).GetComponent<TMP_Text>();
            tempText.text = PlayerPrefs.GetInt("WordleGamesPlayed" + count.ToString(),0).ToString();
            count++;
        }

        count = 3;
        for (int i = 1; i < 17; i += 2)
        {
            tempText = wordleScores.GetChild(i).GetComponent<TMP_Text>();
            tempText.text = PlayerPrefs.GetInt("WordleGamesWon" + count.ToString(),0).ToString();
            count++;
        }
    }
}