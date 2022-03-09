using System;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

namespace Akali.Scripts.Core
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Awake()
        {
            GameStateManager.Instance.GameStatePlaying.OnExecute += LookAt;
        }

        private void LookAt()
        {
            if (Camera.main != null) transform.LookAt(Camera.main.transform);
        }
    }
}
