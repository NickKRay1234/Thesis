using DG.Tweening;
using UnityEngine;

namespace Code.StateMachine
{
    public sealed class SettingsState : AbstractState
    {
        private readonly GameObject _settingsScreen;
        public SettingsState(GameStateMachine gameStateMachine, GameObject settingsScreen) : 
            base(gameStateMachine) => _settingsScreen = settingsScreen;

        public override void Enter()
        {
            base.Enter();
            PauseGameTime();
            PauseDOTweenAnimations();
            AudioListener.pause = true;
            _settingsScreen.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            ResumeGameTime();
            ResumeDOTweenAnimations();
            AudioListener.pause = false;
            _settingsScreen.SetActive(false);
        }
        
        private void PauseGameTime() => 
            Time.timeScale = 0;
        private void ResumeGameTime() => 
            Time.timeScale = 1;
        
        private void PauseDOTweenAnimations()
        {
            DOTween.PauseAll();
            DOTween.ManualUpdate(0, 0);
        }

        private void ResumeDOTweenAnimations()
        {
            DOTween.PlayAll();
            DOTween.ManualUpdate(0, 0);
        }
    }
}