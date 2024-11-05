using System;
using UnityEngine;

namespace Code.StateMachine
{
    public sealed class MenuState : AbstractState
    {
        private readonly Menu _menu;
        public MenuState(GameStateMachine gameStateMachine, Menu menu) : base(gameStateMachine)
        {
            _menu = menu ?? throw new ArgumentNullException(nameof(menu));
            gameStateMachine = gameStateMachine ?? throw new ArgumentNullException(nameof(gameStateMachine));
            _menu.GameStateMachine = gameStateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            SetMenuActiveState(true);
        }
        
        public override void Exit()
        {
            base.Exit();
            SetMenuActiveState(false);
        }
        
        private void SetMenuActiveState(bool isActive)
        {
            foreach (Transform child in _menu.transform)
                child.gameObject.SetActive(isActive);
        }
    }
}