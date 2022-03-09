using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            other.GetComponent<Keycaps>().isCollect = false;
            ParentPlayer.instance.RemoveDummy(other.gameObject);
        }
    }
}
