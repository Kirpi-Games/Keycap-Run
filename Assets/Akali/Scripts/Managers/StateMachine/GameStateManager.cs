using Akali.Common;
using Akali.Scripts.Managers.StateMachine.States;

namespace Akali.Scripts.Managers.StateMachine
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        #region Game States

        public GameStateBase CurrentState;
        public readonly GameStateMainMenu GameStateMainMenu;
        public readonly GameStatePlaying GameStatePlaying;
        public readonly GameStateComplete GameStateComplete;
        public readonly GameStateFail GameStateFail;

        public GameStateManager()
        {
            GameStateMainMenu = new GameStateMainMenu();
            GameStatePlaying = new GameStatePlaying();
            GameStateComplete = new GameStateComplete();
            GameStateFail = new GameStateFail();
        }

        public void SetGameState(GameStateBase gameState)
        {
            CurrentState?.Exit();
            CurrentState = gameState;
            gameState.Enter();
        }

        #endregion

        private void Start()
        {
            CurrentState = GameStateMainMenu;
            CurrentState.Enter();
        }

        private void Update()
        {
            CurrentState?.Execute();
        }

        private void OnDestroy()
        {
            CurrentState?.Exit();
        }
    }
}