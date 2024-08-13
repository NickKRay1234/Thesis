using UnityEngine;

namespace Code.StateMachine
{
    public sealed class GameController : MonoBehaviour
    {
        [SerializeField] 
        private GameObject _tutorialHand;
        private IState currentState;

        public GameObject TutorialHand => _tutorialHand;

        private void Start() =>
            ChangeState(new TutorialState(this));

        private void Update() => currentState?.Execute();

        public void ChangeState(IState newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
    }
}