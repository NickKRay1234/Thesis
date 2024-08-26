using Code.Gameplay.Movement;
using Code.StateMachine;
using UnityEngine;
using Zenject;

public sealed class LoseScreenActivator : MonoBehaviour
{
    private RotateOnClick _rotateOnClick;

    [Inject] 
    private GameStateMachine _stateMachine;

    [SerializeField] 
    private GameObject _loseScreen;

    private void Start() =>
        _rotateOnClick = GetComponent<RotateOnClick>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Player>()) return;
        if (!_rotateOnClick.IsCorrectAngle(transform.eulerAngles.y))
            _stateMachine.ChangeState(new LoseState(_stateMachine, _loseScreen));
    }
}
