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

    //private double houseTemp;
    //private double indexTemp;

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

        column = GameArea.GetComponent<GameArea>().CalculateColumn(house, index);
        row = GameArea.GetComponent<GameArea>().CalculateRow(house, index);
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
