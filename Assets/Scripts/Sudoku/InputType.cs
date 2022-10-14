using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputType : MonoBehaviour
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
        value = transform.name.Replace("Button","").ToLower();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateGameArea()
    {
        transform.parent.parent.GetComponent<GameArea>().UpdateInputType(value);
    }
}
