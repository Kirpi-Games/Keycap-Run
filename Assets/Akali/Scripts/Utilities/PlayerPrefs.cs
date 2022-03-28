namespace Akali.Scripts.Utilities
{
    public static class PlayerPrefs
    {
        #region Level

        public static int GetLevel()
        {
            if (!UnityEngine.PlayerPrefs.HasKey(Constants.PrefsLevel))
            {
                UnityEngine.PlayerPrefs.SetInt(Constants.PrefsLevel, 1);
                return UnityEngine.PlayerPrefs.GetInt(Constants.PrefsLevel);
            }

            return UnityEngine.PlayerPrefs.GetInt(Constants.PrefsLevel);
        }

        public static void SetLevel(int value)
        {
            UnityEngine.PlayerPrefs.SetInt(Constants.PrefsLevel, value);
        }

        #endregion

        #region LevelText

        public static int GetLevelText()
        {
            if (!UnityEngine.PlayerPrefs.HasKey(Constants.PrefsLevelText))
            {
                UnityEngine.PlayerPrefs.SetInt(Constants.PrefsLevelText, 1);
                return UnityEngine.PlayerPrefs.GetInt(Constants.PrefsLevelText);
            }

            return UnityEngine.PlayerPrefs.GetInt(Constants.PrefsLevelText);
        }

        public static void SetLevelText(int value)
        {
            UnityEngine.PlayerPrefs.SetInt(Constants.PrefsLevelText, value);
        }

        #endregion

        #region Money

        public static int GetMoney()
        {
            if (!UnityEngine.PlayerPrefs.HasKey(Constants.PrefsMoney))
            {
                UnityEngine.PlayerPrefs.SetInt(Constants.PrefsMoney, 0);
                return UnityEngine.PlayerPrefs.GetInt(Constants.PrefsMoney);
            }

            return UnityEngine.PlayerPrefs.GetInt(Constants.PrefsMoney);
        }

        public static void SetMoney(int value)
        {
            UnityEngine.PlayerPrefs.SetInt(Constants.PrefsMoney, value);
        }

        #endregion

        #region Haptic

        public static string GetHaptic()
        {
            if (UnityEngine.PlayerPrefs.HasKey(Constants.PrefsHaptic))
            {
                UnityEngine.PlayerPrefs.SetString(Constants.PrefsHaptic, Constants.BoolVarTrue);
                return UnityEngine.PlayerPrefs.GetString(Constants.PrefsHaptic);
            }

            return UnityEngine.PlayerPrefs.GetString(Constants.PrefsHaptic);
        }

        public static void SetHaptic(string value)
        {
            UnityEngine.PlayerPrefs.SetString(Constants.PrefsHaptic, value);
        }

        #endregion
    }
}