using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground : MonoBehaviour
{
    public float horizontalSpeed = 0.08f;
    public float verticalSpeed = 0.15f;
    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset = new Vector2(Time.time * horizontalSpeed, Time.time * verticalSpeed);
        rend.material.mainTextureOffset = offset;
    }
}