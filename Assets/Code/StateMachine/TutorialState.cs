using UnityEngine;
using Zenject;

namespace Code.StateMachine
{
    public sealed class TutorialState : AbstractState
    {

        public override void Execute()
        {
            base.Execute();
            if(Input.anyKeyDown)
                GameStateMachine.ChangeState(new GameplayState(GameStateMachine));
        }

        public TutorialState(GameStateMachine gameStateMachine) : base(gameStateMachine)
        {
        }
    }
}