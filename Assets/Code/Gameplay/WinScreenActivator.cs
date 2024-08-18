using Code.Gameplay.Movement;
using Code.StateMachine;
using UnityEngine;
using Zenject;

namespace Code.Gameplay
{
    public sealed class WinScreenActivator : MonoBehaviour
    {
        [Inject] 
        private GameStateMachine _stateMachine;
        
        [SerializeField] 
        private GameObject _winScreen;

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<Player>()) return;
            _stateMachine.ChangeState(new WinState(_stateMachine, _winScreen));
        }
    }
}
