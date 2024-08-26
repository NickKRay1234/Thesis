using System;
using Code.Gameplay.Movement;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Code.StateMachine
{
    public sealed class GameStateMachine : ITickable, IInitializable
    {
        [Inject] private IPlayer _player;
        [Inject] private GameplayState _gameplayState;
        [Inject] private TutorialScreen _tutorialScreen;

        public IPlayer Player => _player;
        public IState CurrentState { get; private set; }

        public void ChangeState(IState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
        
        public void Tick() => CurrentState?.Execute();

        public void Initialize() =>
            ChangeState(new TutorialState(this, _tutorialScreen));
        
        public void ChangeOnGameplayState() =>
            ChangeState(_gameplayState);
    }
}