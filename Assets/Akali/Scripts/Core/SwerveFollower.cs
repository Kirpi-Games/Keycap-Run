using Akali.Common;
using UnityEngine;

namespace Akali.Scripts.Core
{
    public class CameraPlayerFollower : Singleton<CameraPlayerFollower>
    {
        private const float FollowSpeed = 0.125f;
        private Vector3 followOffset;

        private void Start()
        {
            followOffset = Vector3.zero + transform.position;
        }

        public void LateUpdate()
        {
            var desiredPosition = SwerveController.Instance.transform.position + followOffset;
            var followPosition = Vector3.Lerp(transform.position, desiredPosition, FollowSpeed);
            transform.position = followPosition;
        }
    }
}