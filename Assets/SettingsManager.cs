using Code.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class SettingsManager : MonoBehaviour
{
    [Inject]
    private GameStateMachine _stateMachine;

    [SerializeField] 
    private GameObject _settingsScreen;

    [SerializeField]
    private Button _settingsButton;

    private void Awake() =>
        _settingsButton.onClick.AddListener(ActivateSettings);

    private void ActivateSettings()
    {
        _stateMachine.ChangeState(new SettingsState(_stateMachine, _settingsScreen));
        OnDeactivatedButtonClicked();
    }

    private void OnDeactivatedButtonClicked()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.AddListener(DeactivateSettings);
    }

    private void OnActivatedButtonClicked()
    {
        _settingsButton.onClick.RemoveAllListeners();
        _settingsButton.onClick.AddListener(ActivateSettings);
    }

    private void DeactivateSettings()
    {
        _stateMachine.ChangeOnGameplayState();
        OnActivatedButtonClicked();
    }
}
