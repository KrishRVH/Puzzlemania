using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SetPuzzle : MonoBehaviour
{
    public void PlaySet()
    {
        transform.GetComponent<SetLayout>().StartSet();
    }

    private void CreateIterations()
    {
        /*
            .               0           1               2
            Number  (#):    One     |   Two       |     Three
            Filling (F):    Empty   |   Full      |     Striped
            Color   (C):    Red     |   Green     |     Purple
            Shape   (S):    Capsule |   Diamond   |     Squiggle

            Card Value = #FCS

            Examples:
                2010 = Three Empty Green Capsule
                1102 = Two Full Red Squiggle
                0021 = One Empty Purple Diamond

        */
    }
}