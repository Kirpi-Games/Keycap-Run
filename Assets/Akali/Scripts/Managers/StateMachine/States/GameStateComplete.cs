namespace Akali.Scripts.Managers.StateMachine.States
{
    public class GameStateComplete : GameStateBase
    {
        public GameEvents.GameStateEvent OnEnter;
        public GameEvents.GameStateEvent OnExecute;
        public GameEvents.GameStateEvent OnExit;

        public override void Enter()
        {
            OnEnter?.Invoke();
        }

        public override void Execute()
        {
            OnExecute?.Invoke();
        }

        public override void Exit()
        {
            OnExit?.Invoke();
        }
    }
}