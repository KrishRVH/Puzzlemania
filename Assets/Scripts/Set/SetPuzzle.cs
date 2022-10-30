using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SetPuzzle : MonoBehaviour
{

    public class Card
    {
        public int Number;
        public int Filling;
        public int Color;
        public int Shape;

        public Card(int Number, int Filling, int Color, int Shape)
        {
            /*
                .               0               1               2
                Number  (#):    One         |   Two         |   Three
                Filling (F):    Empty (E)   |   Full (F)    |   Striped (S)
                Color   (C):    Green (G)   |   Purple (P)  |   Red (R)
                Shape   (S):    Capsule (C) |   Diamond (D) |   Squiggle (S)

                Card Value = #FCS

                Examples:
                    2010 = Three Empty Green Capsule
                    1102 = Two Full Red Squiggle
                    0021 = One Empty Purple Diamond

            */
            this.Number = Number;
            this.Filling = Filling;
            this.Color = Color;
            this.Shape = Shape;
        }
    }

    public Sprite blank;
    public Sprite s000;
    public Sprite s001;
    public Sprite s002;
    public Sprite s010;
    public Sprite s011;
    public Sprite s012;
    public Sprite s020;
    public Sprite s021;
    public Sprite s022;
    public Sprite s100;
    public Sprite s101;
    public Sprite s102;
    public Sprite s110;
    public Sprite s111;
    public Sprite s112;
    public Sprite s120;
    public Sprite s121;
    public Sprite s122;
    public Sprite s200;
    public Sprite s201;
    public Sprite s202;
    public Sprite s210;
    public Sprite s211;
    public Sprite s212;
    public Sprite s220;
    public Sprite s221;
    public Sprite s222;
    public List<Card> cardList;
    public List<Card> selectedList;
    private int setsFound;

    public void PlaySet()
    {
        cardList = new List<Card>();
        selectedList = new List<Card>();
        setsFound = 0;
        CreateIterations();
        transform.GetComponent<SetLayout>().StartSet();
        initialFill();
    }

    private void CreateIterations()
    {
        /*
            .               0               1               2
            Number  (#):    One         |   Two         |   Three
            Filling (F):    Empty (E)   |   Full (F)    |   Striped (S)
            Color   (C):    Green (G)   |   Purple (P)  |   Red (R)
            Shape   (S):    Capsule (C) |   Diamond (D) |   Squiggle (S)

            Card Value = #FCS

            Examples:
                2010 = Three Empty Green Capsule
                1102 = Two Full Red Squiggle
                0021 = One Empty Purple Diamond

        */

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    for (int l = 0; l < 3; l++)
                    {
                        Card cardTemp = new Card(i, j, k, l);
                        cardList.Add(cardTemp);
                    }
                }
            }
        }
    }

    public Card GetRandomCard()
    {
        if (cardList.Count != 0)
        {
            int rand = cardList.Count + 1;
            while (rand >= cardList.Count) { rand = (int)(Random.value * cardList.Count); }
            Card tempCard = cardList[rand];
            RemoveCardFromList(tempCard);
            return tempCard;
        }
        else
        {
            CreateIterations();
            for (int i = 0; i < 12; i++)
            {
                RemoveCardFromList(transform.GetChild(i).GetComponent<SetCard>().GetCardSymbol());
            }
            int rand = cardList.Count + 1;
            while (rand >= cardList.Count) { rand = (int)(Random.value * cardList.Count); }
            Card tempCard = cardList[rand];
            RemoveCardFromList(tempCard);
            return tempCard;
        }
    }

    public void RemoveCardFromList(Card card)
    {
        cardList.Remove(card);
    }

    public void initialFill()
    {
        for (int i = 0; i < 12; i++)
        {
            FillCard(i);
        }
        UpdateAvailableSets();
    }

    public void FillCard(int index)
    {
        Card randCard = GetRandomCard();
        Sprite sprite = GetSprite(randCard.Filling, randCard.Color, randCard.Shape);
        if (randCard.Number == 0)
        {
            transform.GetChild(index).GetComponent<SetCard>().SetCardSymbol(randCard, blank, blank, sprite, blank, blank);
        }
        else if (randCard.Number == 1)
        {
            transform.GetChild(index).GetComponent<SetCard>().SetCardSymbol(randCard, blank, sprite, blank, sprite, blank);
        }
        else
        {
            transform.GetChild(index).GetComponent<SetCard>().SetCardSymbol(randCard, sprite, blank, sprite, blank, sprite);
        }
    }

    public void UpdateSelectedList(Card card, bool selected)
    {
        if (selected)
        {
            selectedList.Add(card);
            if (selectedList.Count == 3)
            {
                IsValidSet(selectedList);
                selectedList.Clear();
            }
        }
        else
        {
            selectedList.Remove(card);
        }
    }
    
    public void IsValidSet(List<Card> list)
    {
        List<int> cardValues = new List<int>();
        int[] temp = {0, 0, 0, 0};
        cardValues.AddRange(temp);
        foreach (Card card in list)
        {
            cardValues[0] += card.Number;
            cardValues[1] += card.Filling;
            cardValues[2] += card.Color;
            cardValues[3] += card.Shape;
        }

        foreach (int number in cardValues)
        {
            if (number % 3 != 0)
            {
                foreach (Card card in list)
                {
                    transform.GetChild(GetCardIndex(card)).GetComponent<SetCard>().ShowInvalidSet();
                }
                return;
            }
        }

        foreach (Card card in list)
        {
            transform.GetChild(GetCardIndex(card)).GetComponent<SetCard>().ShowValidSet();
        }
        SwapCards(selectedList);
        setsFound++;
        UpdateSetsFound();
    }

    public bool isValid(List<Card> list)
    {
        List<int> cardValues = new List<int>();
        int[] temp = {0, 0, 0, 0};
        cardValues.AddRange(temp);
        foreach (Card card in list)
        {
            cardValues[0] += card.Number;
            cardValues[1] += card.Filling;
            cardValues[2] += card.Color;
            cardValues[3] += card.Shape;
        }

        foreach (int number in cardValues)
        {
            if (number % 3 != 0)
            {
                return false;
            }
        }

        return true;
    }

    private void SwapCards(List<Card> list)
    {
        foreach (Card card in list)
        {
            FillCard(GetCardIndex(card));
        }
        UpdateAvailableSets();
    }

    private int GetCardIndex(Card card)
    {
        for (int i = 0; i < 12; i++)
        {
            if (card == transform.GetChild(i).GetComponent<SetCard>().GetCardSymbol())
            {
                return i;
            }
        }
        return 0;
    }

    public int CountAvailableSets()
    {
        int availableSets = 0;
        List<Card> tempList = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            Card one = transform.GetChild(i).GetComponent<SetCard>().GetCardSymbol();
            tempList.Add(one);
            for (int j = (i + 1); j < 11; j++)
            {
                Card two = transform.GetChild(j).GetComponent<SetCard>().GetCardSymbol();
                tempList.Add(two);
                for (int k = (j + 1); k < 12; k++)
                {
                    Card three = transform.GetChild(k).GetComponent<SetCard>().GetCardSymbol();
                    tempList.Add(three);

                    if (isValid(tempList)) { availableSets++; }
                    tempList.Remove(three);
                }
                tempList.Remove(two);
            }
            tempList.Remove(one);
        }
        if (availableSets == 0)
        {
            initialFill();
            return CountAvailableSets();
        }
        return availableSets;
    }

    public void UpdateAvailableSets()
    {
        GameObject.Find("BottomPanel").transform.GetComponent<BottomPanel>().UpdateSetsAvailableTextBox(CountAvailableSets());
    }

    public void UpdateSetsFound()
    {
        GameObject.Find("BottomPanel").transform.GetComponent<BottomPanel>().UpdateSetsFoundTextBox(setsFound);
    }

    public Sprite GetSprite(int filling, int color, int shape)
    {
        switch((filling * 100) + (color * 10) + shape)
        {
            case 000: return s000;
            case 001: return s001;
            case 002: return s002;
            case 010: return s010;
            case 011: return s011;
            case 012: return s012;
            case 020: return s020;
            case 021: return s021;
            case 022: return s022;
            case 100: return s100;
            case 101: return s101;
            case 102: return s102;
            case 110: return s110;
            case 111: return s111;
            case 112: return s112;
            case 120: return s120;
            case 121: return s121;
            case 122: return s122;
            case 200: return s200;
            case 201: return s201;
            case 202: return s202;
            case 210: return s210;
            case 211: return s211;
            case 212: return s212;
            case 220: return s220;
            case 221: return s221;
            case 222: return s222;
            default: return s000;
        }
    }
}