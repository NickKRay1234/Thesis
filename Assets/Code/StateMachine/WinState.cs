using Code.StateMachine;

namespace StateMachine
{
    public sealed class WinState : AbstractState
    {
        public WinState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        
        public override void Execute()
        {
            base.Execute();
        }
        
        public override void Exit()
        {
            base.Exit();
        }
    }
}