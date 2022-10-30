using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrossSumResult : MonoBehaviour
{
    private int index;
    private TMP_Text textBox;
    private Image image;
    private Color defaultColor;
    private Transform gameArea;
    private Transform numbers;

    void Start()
    {
        index = int.Parse(transform.name);
        textBox = transform.GetChild(0).GetComponent<TMP_Text>();
        image = transform.GetComponent<Image>();
        defaultColor = image.color;
        gameArea = transform.parent.parent;
        numbers = gameArea.GetChild(2);
        GetValue();
    }

    private void GetValue()
    {
        textBox.text = (gameArea.GetComponent<CrossSumPuzzle>().results[index]).ToString();
    }

    IEnumerator ValidResultAnimation()
    { 
        image.color = Color.green;
        yield return new WaitForSeconds(1f);
        image.color = defaultColor;
    }

    public void ShowValidResult()
    {
        image.color = Color.green;
        //StartCoroutine(ValidResultAnimation());
    }

    IEnumerator InvalidResultAnimation()
    { 
        image.color = Color.red;
        yield return new WaitForSeconds(1f);
        image.color = defaultColor;
    }

    public void ShowInvalidResult()
    {
        image.color = Color.red;
        //StartCoroutine(InvalidResultAnimation());
    }

    public void ResetColor()
    {
        image.color = defaultColor;
        //StartCoroutine(InvalidResultAnimation());
    }
}