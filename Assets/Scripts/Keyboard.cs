using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
    public static Keyboard instance;
    public List<GameObject> keyCapHolders;

    private void Awake()
    {
        instance = this;
        foreach (var dummy in transform.GetComponentsInChildren<BoxCollider>())
        {
            keyCapHolders.Add(dummy.gameObject);
        }
    }
}
