using UnityEngine;

namespace Code.StateMachine
{
    public sealed class LoseState : AbstractState
    {
        private readonly GameObject _loseScreen;
        public LoseState(GameStateMachine gameStateMachine, GameObject loseScreen) : base(gameStateMachine) => 
            _loseScreen = loseScreen;

        public override void Enter()
        {
            base.Enter();
            _loseScreen.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            _loseScreen.SetActive(false);
        }
    }
}