using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Digits : MonoBehaviour
{

    private string value;
    private Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            UpdateGameArea();
        });
        value = transform.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameArea()
    {
        if (toggle.isOn)
        {
            transform.parent.parent.GetComponent<GameArea>().UpdateToggle(value);
        }
        else 
        {
            transform.parent.parent.GetComponent<GameArea>().UpdateToggle("");
        }
        
    }
}
