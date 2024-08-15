using UnityEngine;

namespace Code.StateMachine
{
    public class AbstractState : IState
    {
        protected readonly GameStateMachine GameStateMachine;

        protected AbstractState(GameStateMachine gameStateMachine) => 
            GameStateMachine = gameStateMachine;

        public virtual void Enter()
        {
#if UNITY_EDITOR
            Debug.Log("<color=green>Entering " + GetType().Name + "</color>");
#endif
        }

        public virtual void Execute()
        {
            
        }

        public virtual void Exit()
        {
#if UNITY_EDITOR
            Debug.Log("<color=yellow>Exiting " + GetType().Name + "</color>");
#endif
        }
    }
}