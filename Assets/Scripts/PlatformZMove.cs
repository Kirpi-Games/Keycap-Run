using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

public class PlatformZMove : MonoBehaviour
{
    public static PlatformZMove instance;
    public float speed;

    private void Awake()
    {
        instance = this;
        GameStateManager.Instance.GameStatePlaying.OnExecute += MovePlatform;
    }

    void MovePlatform()
    {
        transform.Translate(-Vector3.forward * (speed * Time.deltaTime));
    }
}
