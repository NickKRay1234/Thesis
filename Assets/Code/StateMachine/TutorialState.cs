using UnityEngine;

namespace Code.StateMachine
{
    public sealed class TutorialState : AbstractState
    {
        
        public TutorialState(GameController gameController) : base(gameController)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }
        
        public override void Execute()
        {
            base.Execute();
            if(Input.anyKeyDown)
                GameController.ChangeState(new GameplayState(GameController));
        }
        
        public override void Exit()
        {
            base.Exit();
        }
    }
}