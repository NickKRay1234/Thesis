using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace Code.StateMachine
{
    public sealed class GameplayState : AbstractState
    {
        private readonly List<RotatableRail> _rotateOnClickInstances;

        [Inject]
        public GameplayState(GameStateMachine gameStateMachine, List<RotatableRail> rotateOnClickInstances) 
            : base(gameStateMachine) =>
            _rotateOnClickInstances = rotateOnClickInstances;


        public override void Execute()
        {
            base.Execute();
            if (!GameStateMachine.Player.GetSplineMover().IsMoving) return;
            GameStateMachine.Player.GetSplineMover().UpdateSplineMovement();
            if (GameStateMachine.Player.IsPlayerOnRotatableRail) return;
            
            // TODO: Swipe logic
            foreach (var rotateOnClick in _rotateOnClickInstances.Where(rotateOnClick => Input.GetMouseButtonDown(0) && !rotateOnClick.IsRotating))
                rotateOnClick.StartRotatableRailsRotate();
        }

        public override void Exit()
        {
            base.Exit();
            foreach (var rotateOnClick in _rotateOnClickInstances) 
                rotateOnClick.CancelRotation();
        }
    }
}