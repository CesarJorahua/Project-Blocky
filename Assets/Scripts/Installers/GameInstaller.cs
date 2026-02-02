using UnityEngine;
using Zenject;
using JetBrains.Annotations;
using ProjectBlocky.Managers;

namespace Installers
{
    [UsedImplicitly]
    public class GameInstaller : MonoInstaller
    {
        [SerializeField, UsedImplicitly]
        private GridManager gridManager;

        [SerializeField, UsedImplicitly]
        private InputManager inputManager;

        public override void InstallBindings()
        {
            Container.Bind<GridManager>().FromInstance(gridManager).AsSingle().NonLazy();
            Container.Bind<InputManager>().FromInstance(inputManager).AsSingle().NonLazy();
            Container.Bind<ScoreManager>().AsSingle().NonLazy();
            Container.Bind<MoveManager>().AsSingle().NonLazy();
            Container.Bind<GameManager>().AsSingle().NonLazy();
        }
    } 
}