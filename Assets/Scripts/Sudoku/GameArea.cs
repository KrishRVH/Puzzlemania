using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{

    public string toggleValue;
    public string inputType;

    // Start is called before the first frame update
    void Start()
    {
        toggleValue = "0";
        inputType = "numbers";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateToggle(string input)
    {
        toggleValue = input;
    }

    public void UpdateInputType(string input)
    {
        inputType = input;
    }
}
