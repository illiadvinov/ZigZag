using CodeBase.Audio;
using CodeBase.Camera;
using CodeBase.Crystals;
using CodeBase.Infrastructure.Assets;
using CodeBase.Infrastructure.CoroutineStart;
using CodeBase.Infrastructure.Events;
using CodeBase.Infrastructure.Input;
using CodeBase.Infrastructure.StateMachine;
using CodeBase.Infrastructure.StateMachine.States;
using CodeBase.Platforms;
using CodeBase.Player;
using CodeBase.Spawn;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class MainSceneInstaller : MonoInstaller
    {
        [SerializeField] private CoroutineRunner coroutineRunner;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Transform platformsContainer;
        [SerializeField] private Transform crystalsContainer;
        [SerializeField] private GameObject startPlatform;
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject scoreCanvas;
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private AudioSource clickAudioSource;
        [SerializeField] private AudioSource crystalAudioSource;
        [SerializeField] private AudioSource gameOverAudioSource;

        public override void InstallBindings()
        {
            BindStateMachine();
            BindStates();
            BindServices();
            BindContainers();
            BindGameObjects();
            BindCanvases();
            BindAudioSources();
        }

        private void BindAudioSources()
        {
            Container.Bind<AudioSource>().WithId("GameOverAudioSource").FromInstance(gameOverAudioSource);
            Container.Bind<AudioSource>().WithId("CrystalAudioSource").FromInstance(crystalAudioSource);
            Container.Bind<AudioSource>().WithId("ClickAudioSource").FromInstance(clickAudioSource);
        }

        private void BindCanvases()
        {
            Container.Bind<GameObject>().WithId("MainMenuCanvas").FromInstance(mainMenuCanvas);
            Container.Bind<GameObject>().WithId("ScoreCanvas").FromInstance(scoreCanvas);
            Container.Bind<GameObject>().WithId("GameOverCanvas").FromInstance(gameOverCanvas);
        }

        private void BindGameObjects()
        {
            Container.Bind<GameObject>().WithId("StartPlatform").FromInstance(startPlatform);
        }

        private void BindContainers()
        {
            Container.Bind<Transform>().WithId("PlatformsContainer").FromInstance(platformsContainer);
            Container.Bind<Transform>().WithId("CrystalsContainer").FromInstance(crystalsContainer);
        }

        private void BindServices()
        {
            Container.Bind<PlatformManager>().AsSingle();
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
            Container.Bind<ICoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
            Container.Bind<InputService>().AsSingle();
            Container.Bind<EventReferer>().AsSingle();
            Container.Bind<CameraService>().AsSingle();
            Container.Bind<PlayerProgress>().AsSingle();
            Container.Bind<IPool>().To<CrystalPool>().AsSingle();
            Container.Bind<SpawnerManager>().AsSingle();
            Container.Bind<CrystalSpawner>().AsSingle();
            Container.Bind<PlayerMovement>().FromInstance(playerMovement).AsSingle();
            Container.Bind<AudioManager>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<IState>().WithId("InitializeState").To<InitializeState>().AsSingle();
            Container.Bind<IState>().WithId("GamePlayState").To<GamePlayState>().AsSingle();
            Container.Bind<IState>().WithId("FinalState").To<FinalState>().AsSingle();
            Container.Bind<IState>().WithId("MenuState").To<MenuState>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateMachine>().To<GameStateMachine>().AsSingle();
        }
    }
}