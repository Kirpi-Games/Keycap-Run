using Akali.Common;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

namespace Akali.Scripts
{
    public class SwerveController : Singleton<SwerveController>
    {
        [SerializeField, Range(0, 10)] private float swerveClamp = 5f;
        [SerializeField, Range(0, 10)] private float swerveSpeed = 2f;
        private float lastPosition;

        private void Awake()
        {
            GameStateManager.Instance.GameStateMainMenu.OnExecute += StartSwerve;
            GameStateManager.Instance.GameStatePlaying.OnExecute += Swerve;
        }

        // private void DisableSwerve()
        // {
        //     GameStateManager.Instance.GameStateMainMenu.onExecute -= StartSwerve;
        //     GameStateManager.Instance.GameStatePlaying.onExecute -= Swerve;
        // }

        private void StartSwerve()
        {
            if (Input.GetMouseButtonDown(0))
            {
                lastPosition = Input.mousePosition.x;
                AkaliLevelManager.Instance.LevelIsPlaying();
            }
        }

        private void Swerve()
        {
            if (Input.GetMouseButtonDown(0)) lastPosition = Input.mousePosition.x;
            
            if (Input.GetMouseButton(0))
            {
                if (lastPosition == 0)
                {
                    lastPosition = Input.mousePosition.x;
                }
                var currentPosition = Input.mousePosition.x;
                var deltaPosition = currentPosition - lastPosition;
                var targetPosition = deltaPosition * swerveSpeed * Time.deltaTime;
                var desiredPosition = transform.position + Vector3.right * (targetPosition);
                desiredPosition.x = Mathf.Clamp(desiredPosition.x, -swerveClamp, swerveClamp);
                transform.position = desiredPosition;
                lastPosition = Input.mousePosition.x;
            }
        }
    }
}