using UnityEngine;

namespace Code.StateMachine
{
    public sealed class TutorialState : AbstractState
    {
        private readonly TutorialScreen _tutorialScreen;
        
        public TutorialState(GameStateMachine gameStateMachine, TutorialScreen tutorialScreen) : base(gameStateMachine) => 
            _tutorialScreen = tutorialScreen;

        public override void Enter()
        {
            base.Enter();
            _tutorialScreen.gameObject.SetActive(true);
        }
        
        public override void Execute()
        {
            base.Execute();
            if(Input.anyKeyDown)
                GameStateMachine.ChangeOnGameplayState();
        }
        
        public override void Exit()
        {
            base.Exit();
            _tutorialScreen.gameObject.SetActive(false);
        }
    }
}