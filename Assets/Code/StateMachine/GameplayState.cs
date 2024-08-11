namespace Code.StateMachine
{
    public sealed class GameplayState : AbstractState
    {
        public GameplayState(GameController gameController) : base(gameController) { }
        public override void Enter() => base.Enter();
        public override void Execute() => base.Execute();
        public override void Exit() => base.Exit();
    }
}