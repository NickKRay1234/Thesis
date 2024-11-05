using Code.Gameplay.Movement;
using Zenject;
using IInitializable = Zenject.IInitializable;

namespace Code.StateMachine
{
    public sealed class GameStateMachine : ITickable, IInitializable
    {
        [Inject] private IPlayer _player;
        [Inject] private GameplayState _gameplayState;
        [Inject] private Menu _menu;

        public IPlayer Player => _player;
        private IState CurrentState { get; set; }

        public void ChangeState(IState newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
        
        public void Tick() => CurrentState?.Execute();

        public void Initialize() =>
            ChangeState(new MenuState(this, _menu));
        
        public void ChangeOnGameplayState() =>
            ChangeState(_gameplayState);
    }
}