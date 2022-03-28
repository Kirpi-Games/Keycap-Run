using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Utilities;
using UnityEngine;

namespace Akali.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelList", menuName = App.AppName + "/Levels/LevelList")]
    public class LevelListScriptableObject : ScriptableObject
    {
        public List<LevelScriptableObject> list;
        public int repeatingLevel;

        public int GetLevelCount()
        {
            return list.Count;
        }

        public LevelScriptableObject GetCurrentLevel(int playerPrefsLevelIndex)
        {
            return list[playerPrefsLevelIndex - 1];
        }

        public void GoToNextLevel(bool goToNextLevel)
        {
            if (goToNextLevel)
            {
                if (Utilities.PlayerPrefs.GetLevel() % GetLevelCount() == 0)
                {
                    Utilities.PlayerPrefs.SetLevel(repeatingLevel);
                    Utilities.PlayerPrefs.SetLevelText(Utilities.PlayerPrefs.GetLevelText() + 1);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.SceneSample);
                }
                else
                {
                    Utilities.PlayerPrefs.SetLevel(Utilities.PlayerPrefs.GetLevel() + 1);
                    Utilities.PlayerPrefs.SetLevelText(Utilities.PlayerPrefs.GetLevelText() + 1);
                    UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.SceneSample);
                }
            }
            else UnityEngine.SceneManagement.SceneManager.LoadScene(Constants.SceneSample);
        }
    }
}