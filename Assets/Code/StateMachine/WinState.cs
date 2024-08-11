using Code.StateMachine;

namespace StateMachine
{
    public sealed class WinState : AbstractState
    {
        public WinState(GameController gameController) : base(gameController)
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