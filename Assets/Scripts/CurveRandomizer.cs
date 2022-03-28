using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using AmazingAssets.CurvedWorld;
using UnityEngine;
using Random = UnityEngine.Random;

public class CurveRandomizer : MonoBehaviour
{
    private CurvedWorldController _curvedWorldController;

    private void Awake()
    {
        _curvedWorldController = GetComponent<CurvedWorldController>();
    }

    private void Start()
    {
        _curvedWorldController.bendHorizontalSize = Random.Range(-9, 9);
    }
}
