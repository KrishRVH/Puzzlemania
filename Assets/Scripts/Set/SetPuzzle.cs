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
}