using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitButton : MonoBehaviour
{

    private Button button;
    private Transform gameArea;
    private Transform guessArea;

    // Start is called before the first frame update
    void Start()
    {
        button = transform.GetComponent<Button>();
        button.onClick.AddListener(SubmitClick);
        gameArea = transform.parent.parent;
        guessArea = gameArea.GetChild(0);
    }

    void SubmitClick()
    {
        guessArea.GetComponent<GuessArea>().SubmitGuess();
    }
}