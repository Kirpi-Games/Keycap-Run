using UnityEditor;
using UnityEngine;

namespace Akali.Common
{
    public static class EditorFunctionality
    {   
#if UNITY_EDITOR
        private static int _screenshotIndex;
        
        [MenuItem(App.AppName + "/Functionality/Clear Player Prefs")]
        public static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
            Debugger.Log("Player Prefs cleared.");
        }
        
        [MenuItem(App.AppName + "/Take Screenshot _F12")]
        public static void TakeScreenshot()
        {
            ScreenCapture.CaptureScreenshot($"{App.AppName}_" + $"{_screenshotIndex}.png");
            Debugger.Log("Screenshot taken.");
            _screenshotIndex += 1;
        }
#endif
    }
}