using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace Code.StateMachine
{
    public sealed class GameplayState : AbstractState
    {
        private readonly List<RotateOnClick> _rotateOnClickInstances;

        [Inject]
        public GameplayState(GameStateMachine gameStateMachine, List<RotateOnClick> rotateOnClickInstances) 
            : base(gameStateMachine) =>
            _rotateOnClickInstances = rotateOnClickInstances;


        public override void Execute()
        {
            base.Execute();
            if (!GameStateMachine.Player.GetSplineMover().IsMoving) return;
            GameStateMachine.Player.GetSplineMover().UpdateSplineMovement();

            foreach (var rotateOnClick in _rotateOnClickInstances.Where(rotateOnClick => Input.GetMouseButtonDown(0) && !rotateOnClick.IsRotating))
                rotateOnClick.StartRotatableRailsRotate();
        }
    }
}