using Code.Gameplay.Movement;
using UnityEngine;
using Zenject;

namespace Code.StateMachine
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] 
        private Player _player;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IPlayer>()
                .FromInstance(_player)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<IMovable>()
                .To<SplineMover>()
                .AsSingle();
        }
    }
}