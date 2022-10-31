using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpPanel : MonoBehaviour
{
    private GameObject master;
    private Image leftHelpImage;
    private Image rightHelpImage;
    public Sprite CrossSumLeftImage;
    public Sprite CrossSumRightImage;
    public Sprite SetLeftImage;
    public Sprite SetRightImage;
    public Sprite SudokuLeftImage;
    public Sprite SudokuRightImage;
    public Sprite WordleLeftImage;
    public Sprite WordleRightImage;

    void OnEnable()
    {
        master = GameObject.Find("Master");
        leftHelpImage = transform.GetChild(1).GetComponent<Image>();
        rightHelpImage = transform.GetChild(2).GetComponent<Image>();
        
        if (master.GetComponent<GameState>().gameChoice == "CrossSum")
        {
            leftHelpImage.sprite = CrossSumLeftImage;
            rightHelpImage.sprite = CrossSumRightImage;
        }
        else if (master.GetComponent<GameState>().gameChoice == "Set")
        {
            leftHelpImage.sprite = SetLeftImage;
            rightHelpImage.sprite = SetRightImage;
        }
        else if (master.GetComponent<GameState>().gameChoice == "Sudoku")
        {
            leftHelpImage.sprite = SudokuLeftImage;
            rightHelpImage.sprite = SudokuRightImage;
        }
        else if (master.GetComponent<GameState>().gameChoice == "Wordle")
        {
            leftHelpImage.sprite = WordleLeftImage;
            rightHelpImage.sprite = WordleRightImage;
        }
    }
}