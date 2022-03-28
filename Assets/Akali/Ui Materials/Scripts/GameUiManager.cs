using Akali.Common;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

namespace Akali.Ui_Materials.Scripts
{
    public class GameUiManager : Singleton<GameUiManager>
    {
        public GameObject mainMenuTutorial;
        public GameObject playingLevel;
        public GameObject playingCoinBar;
        public GameObject background;
        public GameObject completeUi;
        public GameObject completeButton;
        public GameObject failUi;
        public GameObject failButton;
        
        private void Awake()
        {
            GameStateManager.Instance.GameStateMainMenu.OnEnter += SetActiveMainMenuUi;
            GameStateManager.Instance.GameStateMainMenu.OnExit += SetActiveMainMenuUi;
            GameStateManager.Instance.GameStatePlaying.OnEnter += SetActivePlayingUi;
            GameStateManager.Instance.GameStatePlaying.OnExit += SetActivePlayingUi;
            GameStateManager.Instance.GameStateComplete.OnEnter += SetActiveCompleteUi;
            GameStateManager.Instance.GameStateComplete.OnExit += SetActiveCompleteUi;
            GameStateManager.Instance.GameStateFail.OnEnter += SetActiveFailUi;
            GameStateManager.Instance.GameStateFail.OnExit += SetActiveFailUi;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.GameStateMainMenu.OnEnter -= SetActiveMainMenuUi;
            GameStateManager.Instance.GameStateMainMenu.OnExit -= SetActiveMainMenuUi;
            GameStateManager.Instance.GameStatePlaying.OnEnter -= SetActivePlayingUi;
            GameStateManager.Instance.GameStatePlaying.OnExit -= SetActivePlayingUi;
            GameStateManager.Instance.GameStateComplete.OnEnter -= SetActiveCompleteUi;
            GameStateManager.Instance.GameStateComplete.OnExit -= SetActiveCompleteUi;
            GameStateManager.Instance.GameStateFail.OnEnter -= SetActiveFailUi;
            GameStateManager.Instance.GameStateFail.OnExit -= SetActiveFailUi;
        }
        
        public void SetActiveMainMenuUi()
        {
            mainMenuTutorial.SetActive(!mainMenuTutorial.activeSelf);
        }

        public void SetActivePlayingUi()
        {
            playingLevel.SetActive(!playingLevel.activeSelf);
            playingCoinBar.SetActive(!playingCoinBar.activeSelf);
        }

        private void SetActiveCompleteUi()
        {
            background.SetActive(!background.activeSelf);
            completeUi.SetActive(!completeUi.activeSelf);
            completeButton.SetActive(!completeButton.activeSelf);
        }

        private void SetActiveFailUi()
        {
            background.SetActive(!background.activeSelf);
            failUi.SetActive(!failUi.activeSelf);
            failButton.SetActive(!failButton.activeSelf);
        }
    }
}