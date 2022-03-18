using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDoors : MonoBehaviour
{
    public ParticleSystem ps;
    public ParticleSystem psGlow;
    public Color doorColor;
    public Color emColor;
    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = doorColor;
        var main = ps.main;
        var main2 = psGlow.main;
        main.startColor = doorColor;
        main2.startColor = doorColor;
    }
}
