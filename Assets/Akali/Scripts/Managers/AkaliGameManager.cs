using Akali.Common;
using UnityEngine;

namespace Akali.Scripts.Managers
{
    public class AkaliGameManager : Singleton<AkaliGameManager>
    {
        private void Awake()
        {
            if (Application.isMobilePlatform)
            {
                QualitySettings.vSyncCount = 0;
                Application.targetFrameRate = 60;
                Input.multiTouchEnabled = false;
            }
        }
    }
}