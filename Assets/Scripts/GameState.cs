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
    private float volume;

    void Start()
    {
        audioSource = transform.GetComponent<AudioSource>();
        if (PlayerPrefs.GetInt("Audio", 1) == 0)
        {
            volume = 0f;
            audioSource.volume = 0;
        }
        else
        {
            volume = 0.25f;
        }
        audioSource.playOnAwake = true;
        audioSource.loop = true;
        fadeScript = transform.GetComponent<CrossFadeAudioSource>();
        gameChoice = "";
        gameOption = "";
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("Audio", 1) == 0)
        {
            volume = 0f;
            audioSource.volume = 0;
        }
        else
        {
            volume = 0.25f;
        }
        if (volume != 0f)
        {
            if ((gameChoice == "") && (audioSource.clip.name != "MainMenu"))
            {
                fadeScript.Fade(musicMainMenu, volume);
            }
            else if ((gameChoice == "CrossSum") && (audioSource.clip.name != "CrossSum"))
            {
                fadeScript.Fade(musicCrossSum, volume);
            }
            else if ((gameChoice == "Set") && (audioSource.clip.name != "Set"))
            {
                fadeScript.Fade(musicSet, volume);
            }
            else if ((gameChoice == "Sudoku") && (audioSource.clip.name != "Sudoku"))
            {
                fadeScript.Fade(musicSudoku, volume);
            }
            else if ((gameChoice == "Wordle") && (audioSource.clip.name != "Wordle"))
            {
                fadeScript.Fade(musicWordle, volume);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}