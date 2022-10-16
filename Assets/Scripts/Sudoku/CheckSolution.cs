using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CheckSolution : MonoBehaviour
{

    private Transform GameArea;
    private Component GameAreaScript;
    private bool[,] columns;
    private bool[,] rows;
    private bool[,] houses;

    // Start is called before the first frame update
    void Start()
    {
        GameArea = transform.parent.parent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        columns = GameArea.GetComponent<GameArea>().columns;
        rows = GameArea.GetComponent<GameArea>().rows;
        houses = GameArea.GetComponent<GameArea>().houses;
        foreach (bool value in columns)
        {
            if (!value)
            {
                return;
            }
        }

        foreach (bool value in rows)
        {
            if (!value)
            {
                return;
            }
        }

        foreach (bool value in houses)
        {
            if (!value)
            {
                return;
            }
        }
        
        // WIN
    }
}
