using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Movement;
using Zenject;

namespace Code.StateMachine
{
    public sealed class GameplayState : AbstractState
    {
        private readonly List<RotatableRail> _rotateOnClickInstances;
        private readonly ISwipeHandler _swipeHandler;

        [Inject]
        public GameplayState(GameStateMachine gameStateMachine, List<RotatableRail> rotateOnClickInstances,
            ISwipeHandler swipeHandler)
            : base(gameStateMachine)
        {
            _rotateOnClickInstances = rotateOnClickInstances;
            _swipeHandler = swipeHandler;
        }

        public override void Execute()
        {
            base.Execute();

            if (!GameStateMachine.Player.GetSplineMover().IsMoving) return;
            GameStateMachine.Player.GetSplineMover().UpdateSplineMovement();
            if (GameStateMachine.Player.IsPlayerOnRotatableRail) return;

            _swipeHandler.HandleSwipe();

            foreach (var rotateOnClick in _rotateOnClickInstances.Where(rotateOnClick =>
                         _swipeHandler.IsSwiping && !rotateOnClick.IsRotating))
                rotateOnClick.StartRotatableRailRotate(_swipeHandler.IsSwipeRight);
        }

        public override void Exit()
        {
            base.Exit();
            foreach (var rotateOnClick in _rotateOnClickInstances)
                rotateOnClick.CancelRotation();
        }
    }
}