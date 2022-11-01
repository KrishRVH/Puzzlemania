using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public string gameChoice;
    public string gameOption;
    private AudioSource audioSource;
    public AudioClip musicMainMenu;
    public AudioClip musicCrossSum;
    public AudioClip musicSet;
    public AudioClip musicSudoku;
    public AudioClip musicWordle;
    private CrossFadeAudioSource fadeScript;

    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        fadeScript = transform.GetComponent<CrossFadeAudioSource>();
        gameChoice = "";
        gameOption = "";
    }

    void Update()
    {
        if ((gameChoice == "") && (audioSource.clip.name != "MainMenu"))
        {
            fadeScript.Fade(musicMainMenu, 0.25f);
        }
        else if ((gameChoice == "CrossSum") && (audioSource.clip.name != "CrossSum"))
        {
            fadeScript.Fade(musicCrossSum, 0.25f);
        }
        else if ((gameChoice == "Set") && (audioSource.clip.name != "Set"))
        {
            fadeScript.Fade(musicSet, 0.25f);
        }
        else if ((gameChoice == "Sudoku") && (audioSource.clip.name != "Sudoku"))
        {
            fadeScript.Fade(musicSudoku, 0.25f);
        }
        else if ((gameChoice == "Wordle") && (audioSource.clip.name != "Wordle"))
        {
            fadeScript.Fade(musicWordle, 0.25f);
        }
    }
}