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

        [SerializeField, UsedImplicitly]
        private GameManager gameManager;

        public override void InstallBindings()
        {
            Container.Bind<ScoreManager>().AsSingle().NonLazy();
            Container.Bind<MoveManager>().AsSingle().NonLazy();
            Container.Bind<GridManager>().FromInstance(gridManager).AsSingle().NonLazy();
            Container.Bind<GameManager>().FromInstance(gameManager).AsSingle().NonLazy();
            Container.Bind<InputManager>().FromInstance(inputManager).AsSingle().NonLazy();
        }
    } 
}