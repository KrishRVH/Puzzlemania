using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
//[AddComponentMenu("UI/TextMeshPro - Input Field", 11)]

public class Cell : MonoBehaviour, ISelectHandler
{

    private string parentName;
    private string inputType;
    private string toggleValue;
    private TMP_InputField inputField;
    private Transform GameArea;

    public int house;
    public int index;
    public int column;
    public int row;
    public string currentValue;
    public int[] notes;

    private double houseTemp;
    private double indexTemp;

    private bool add;

    // Start is called before the first frame update
    void Start()
    {
        GameArea = transform.parent.parent.parent;
        currentValue = "";
        add = false;
        inputField = GetComponent<TMP_InputField>();
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

    void UpdateCell(string input, bool add)
    {
        if (!add)
        {
            inputField.text = input;
            GameArea.GetComponent<GameArea>().UpdateCellValue(column, row, house, int.Parse(currentValue), add);
            currentValue = "";
        }
        else if (add != GameArea.GetComponent<GameArea>().CheckColumnValue(column,int.Parse(toggleValue)))
        {
            if (add != GameArea.GetComponent<GameArea>().CheckRowValue(row,int.Parse(toggleValue)))
            {
                if (add != GameArea.GetComponent<GameArea>().CheckHouseValue(house,int.Parse(toggleValue)))
                {
                    // Valid placement
                    inputField.text = input;
                    currentValue = toggleValue;
                    GameArea.GetComponent<GameArea>().UpdateCellValue(column, row, house, int.Parse(toggleValue), add);
                }
                else
                {
                    // Invalid House
                    Debug.Log("Invalid House");
                }
            }
            else
            {
                // Invalid Row
                Debug.Log("Invalid Row");
            }
        }
        else
        {
            // Invalid Column
            Debug.Log("Invalid Column");
        }

        add = false;
    }

	public void OnSelect (BaseEventData eventData) 
	{
        inputType = GameArea.GetComponent<GameArea>().inputType;
        toggleValue = GameArea.GetComponent<GameArea>().toggleValue;

        if (inputType == "numbers")
        {
            // Number
            if (toggleValue == "C")
            {
                if (currentValue != "")
                {
                    add = false;
                    UpdateCell("", add);
                }
            }
            else if (toggleValue == "")
            {
                // Do Nothing
            }
            else
            {
                if (currentValue != toggleValue)
                {
                    add = true;
                    UpdateCell(toggleValue, add);
                }
            }
        }
        else
        {
            // Notes
        }

    }
}
