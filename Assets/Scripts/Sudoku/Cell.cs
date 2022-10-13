using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    private string parentName;

    public int house;
    public int index;
    public int column;
    public int row;
    public int[] notes;

    private double houseTemp;
    private double indexTemp;


    // Start is called before the first frame update
    void Start()
    {
        parentName = transform.parent.name;
        index = int.Parse(transform.name);
        house = int.Parse(parentName.Substring(parentName.Length - 1));
        notes = new int[9];

        houseTemp = house % 3;
        indexTemp = (index % 3) / 10.0;
        switch(houseTemp + indexTemp) 
        {
            case 0:
                column = 9;
                break;
            case 0.1:
                column = 7;
                break;
            case 0.2:
                column = 8;
                break;
            case 1:
                column = 3;
                break;
            case 1.1:
                column = 1;
                break;
            case 1.2:
                column = 2;
                break;
            case 2:
                column = 6;
                break;
            case 2.1:
                column = 4;
                break;
            case 2.2:
                column = 5;
                break;
            default:
                break;
        }

        houseTemp = Mathf.Floor((house - 1) / 3);
        indexTemp = Mathf.Floor((index - 1) / 3) / 10.0;
        switch(houseTemp + indexTemp)
        {
            case 0:
                row = 1;
                break;
            case 0.1:
                row = 2;
                break;
            case 0.2:
                row = 3;
                break;
            case 1:
                row = 4;
                break;
            case 1.1:
                row = 5;
                break;
            case 1.2:
                row = 6;
                break;
            case 2:
                row = 7;
                break;
            case 2.1:
                row = 8;
                break;
            case 2.2:
                row = 9;
                break;
            default:
                break;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
