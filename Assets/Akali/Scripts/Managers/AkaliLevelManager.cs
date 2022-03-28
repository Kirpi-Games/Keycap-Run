using Akali.Common;
using Akali.Scripts.Managers.StateMachine;
using Akali.Scripts.ScriptableObjects;
using PlayerPrefs = Akali.Scripts.Utilities.PlayerPrefs;

namespace Akali.Scripts.Managers
{
    public class AkaliLevelManager : Singleton<AkaliLevelManager>
    {
        public LevelListScriptableObject levels;
        public LevelScriptableObject CurrentLevel => levels.GetCurrentLevel(PlayerPrefs.GetLevel());

        private void Awake()
        {
            Instantiate(CurrentLevel.levelPrefab);
            Instantiate(CurrentLevel.playerPrefab);
        }

        public void LevelIsPlaying()
        {
            Taptic.Light();
            GameStateManager.Instance.SetGameState(GameStateManager.Instance.GameStatePlaying);
            TinySauce.OnGameStarted(PlayerPrefs.GetLevelText().ToString());
        }

        public void LevelIsCompleted()
        {
            Taptic.Success();
            GameStateManager.Instance.SetGameState(GameStateManager.Instance.GameStateComplete);
            TinySauce.OnGameFinished(true, PlayerPrefs.GetMoney(), PlayerPrefs.GetLevelText().ToString());
        }

        public void LevelIsFail()
        {
            Taptic.Failure();
            GameStateManager.Instance.SetGameState(GameStateManager.Instance.GameStateFail);
            TinySauce.OnGameFinished(false, PlayerPrefs.GetMoney(), PlayerPrefs.GetLevelText().ToString());
        }
    }
}