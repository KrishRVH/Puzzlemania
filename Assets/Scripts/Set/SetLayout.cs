using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SetLayout : MonoBehaviour
{

    public GameObject cardPrefab;
    public GameObject cardSymbolPrefab;
    private Transform gameArea;

    void Start()
    {
        gameArea = GetComponent<Transform>();
    }

    public void StartSet()
    {
        gameArea = GetComponent<Transform>();
        CreateCardGrid();
    }

    void CreateCardGrid()
    {
        // Grid: 4 cards wide, 3 cards tall
        float cardWidth = 3f;
        float cardHeight = 4f;
        float horizontalSpacing = 0.5f;
        float verticalSpacing = 0.5f;

        int z = 2;
        for (int i = 0; i < 3; i++)
        {
            float y = ((cardHeight + verticalSpacing) * (1 - i));
            for (int j = 0; j < 4; j++)
            {
                float x = ((cardWidth + horizontalSpacing) * (j - 1.5f));
                GameObject tempCard = Instantiate(cardPrefab, new Vector3(x,y,z), Quaternion.identity, gameArea);
                tempCard.name = ((i * 4) + j).ToString();
            }
        }
    }
}