using Code.Gameplay.Movement;
using Zenject;

namespace Code.StateMachine
{
    public sealed class GameStateMachine : ITickable, IInitializable
    {
        [Inject] 
        private IPlayer _player;
        private IState _currentState;
        
        public IPlayer Player => _player;
        
        public void ChangeState(IState newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void Tick() =>
            _currentState?.Execute();
        
        public void Initialize() =>
            ChangeState(new TutorialState(this));
    }
}