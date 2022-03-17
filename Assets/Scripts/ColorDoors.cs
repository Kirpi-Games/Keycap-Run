using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDoors : MonoBehaviour
{
    public Color doorColor;
    public Color emColor;
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = doorColor;
    }
}
