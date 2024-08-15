using UnityEngine;

namespace Code.StateMachine
{
    public sealed class TutorialState : AbstractState
    {

        public override void Execute()
        {
            base.Execute();
            if(Input.anyKeyDown)
                GameStateMachine.ChangeOnGameplayState();
        }

        public TutorialState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }
    }
}