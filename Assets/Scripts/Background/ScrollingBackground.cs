using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float horizontalSpeed = 0.08f;
    public float verticalSpeed = 0.15f;
    private Renderer rend;
    private bool disable;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (PlayerPrefs.GetInt("ScrollingBackgrounds", 1) == 0)
        {
            disable = true;
        }
        else
        {
            disable = false;
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("ScrollingBackgrounds", 1) == 0)
        {
            disable = true;
        }
        else
        {
            disable = false;
        }
        if (!disable)
        {
            Vector2 offset = new Vector2(Time.time * horizontalSpeed, Time.time * verticalSpeed);
            rend.material.mainTextureOffset = offset;
        }
    }
}