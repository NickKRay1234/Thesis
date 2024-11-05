using Code.Gameplay;
using UnityEngine;
using Zenject;

namespace Code.StateMachine
{
    public sealed class Menu : MonoBehaviour
    {
        [Inject] private TutorialScreen _tutorialScreen;
        public GameStateMachine GameStateMachine { get; set; }

        public void StartGameplay() => 
            GameStateMachine.ChangeState(new TutorialState(GameStateMachine, _tutorialScreen));

    }
}