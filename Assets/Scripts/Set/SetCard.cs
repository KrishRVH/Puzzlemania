using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetCard : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;
    private Image image;
    private Transform gameArea;
    private Color defaultColor;
    private Color selectedColor;
    public bool selected;
    private SetPuzzle.Card currentCard;

    void Awake()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(delegate{CardClick(false);});
        image = GetComponent<Image>();
        gameArea = transform.parent;
        defaultColor = Color.white;
        selectedColor = new Color(1f, 1f, 1f, 0.7f);
        selected = false;
        currentCard = new SetPuzzle.Card(0, 0, 0, 0);
    }

    public void CardClick(bool deselect = false)
    {
        if (selected || deselect)
        {
            selected = false;
            image.color = defaultColor;
            if (deselect) { return; }
            transform.parent.GetComponent<SetPuzzle>().UpdateSelectedList(currentCard, false);
        }
        else
        {
            selected = true;
            image.color = selectedColor;
            transform.parent.GetComponent<SetPuzzle>().UpdateSelectedList(currentCard, true);
        }
    }

    private void CopyCard(SetPuzzle.Card from, SetPuzzle.Card to)
    {
        to.Number = from.Number;
        to.Filling = from.Filling;
        to.Color = from.Color;
        to.Shape = from.Shape;
    }

    public void SetCardSymbol(SetPuzzle.Card card, Sprite zero, Sprite one, Sprite two, Sprite three, Sprite four)
    {
        //currentCard = new SetPuzzle.Card(0, 0, 0, 0);
        CopyCard(card, currentCard);
        transform.GetChild(0).GetComponent<Image>().sprite = zero;
        transform.GetChild(1).GetComponent<Image>().sprite = one;
        transform.GetChild(2).GetComponent<Image>().sprite = two;
        transform.GetChild(3).GetComponent<Image>().sprite = three;
        transform.GetChild(4).GetComponent<Image>().sprite = four;
    }

    public SetPuzzle.Card GetCardSymbol()
    {
        return currentCard;
    }
}