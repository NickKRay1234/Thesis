using Code.Gameplay.Movement;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Code.StateMachine
{
    public sealed class GameStateMachine : ITickable, IInitializable
    {
        [Inject] private IPlayer _player;
        [Inject] private GameplayState _gameplayState;
        private IState _currentState;
        
        public IPlayer Player => _player;

        private void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void Tick() =>
            _currentState?.Execute();
        
        public void Initialize() =>
            ChangeState(new TutorialState(this));
        
        public void ChangeOnGameplayState() =>
            ChangeState(_gameplayState);
    }
}