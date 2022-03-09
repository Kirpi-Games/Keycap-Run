using Akali.Common;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

namespace Akali.Scripts.Core
{
    public class MovementController : Singleton<MovementController>
    {
        public bool movement;
        [Range(5, 30)] public float platformMovementSpeed;

        private void Awake()
        {
            GameStateManager.Instance.GameStatePlaying.OnExecute += MoveZ;
        }

        private void MoveZ()
        {
            if (movement)
            {
                transform.Translate(Vector3.back * platformMovementSpeed * Time.deltaTime);
            }
        }
    }
}
