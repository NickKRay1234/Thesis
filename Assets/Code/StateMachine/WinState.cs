using UnityEngine;

namespace Code.StateMachine
{
    public sealed class WinState : AbstractState
    {
        private readonly GameObject _winScreen;
        public WinState(GameStateMachine gameStateMachine, GameObject winScreen) : base(gameStateMachine) => 
            _winScreen = winScreen;

        public override void Enter()
        {
            base.Enter();
            _winScreen.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            _winScreen.SetActive(false);
        }
    }
}