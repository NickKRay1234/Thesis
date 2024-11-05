using Code.Gameplay;
using Code.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace Code.StateMachine
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] 
        private Player _player;

        [SerializeField] 
        private TutorialScreen _tutorialScreen;

        [SerializeField] 
        private Menu _menu;
        
        public override void InstallBindings()
        {
            
            Container
                .Bind<ISwipeHandler>()
                .To<SwipeHandler>()
                .AsSingle();
            
            Container
                .Bind<IPlayer>()
                .FromInstance(_player)
                .AsSingle();
            
            Container
                .Bind<TutorialScreen>()
                .FromInstance(_tutorialScreen)
                .AsSingle();

            
            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<IMovable>()
                .To<SplineMover>()
                .AsSingle();
            
            Container
                .Bind<RotatableRail>()
                .FromComponentsInHierarchy()
                .AsSingle();
            
            Container
                .Bind<GameplayState>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<TutorialState>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<Menu>()
                .FromInstance(_menu)
                .NonLazy();
        }
    }
}