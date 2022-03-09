namespace Akali.Scripts.Managers.StateMachine
{
    public abstract class GameStateBase
    {
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();
    }
}