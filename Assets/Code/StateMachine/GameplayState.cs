using UnityEngine;

namespace Code.StateMachine
{
    public sealed class GameplayState : AbstractState
    {
        
        public GameplayState(GameStateMachine gameStateMachine) : 
            base(gameStateMachine) { }


        public override void Execute()
        {
            base.Execute();
            if (!GameStateMachine.Player.GetSplineMover().IsMoving) return;
            GameStateMachine.Player.GetSplineMover().UpdateSplineMovement();
            
            if(Input.anyKeyDown)
                GameStateMachine.ChangeState(new TutorialState(GameStateMachine));
        }
    }
}