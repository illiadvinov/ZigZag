using CodeBase.Audio;
using CodeBase.Camera;
using CodeBase.Crystals;
using CodeBase.Infrastructure.Events;
using CodeBase.Platforms;
using CodeBase.Player;
using CodeBase.Spawn;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class InitializeState : IState
    {
        private readonly PlatformManager platformManager;
        private readonly IStateMachine stateMachine;
        private readonly CameraService cameraService;
        private readonly PlayerProgress playerProgress;
        private readonly IPool crystalPool;
        private readonly SpawnerManager spawnerManager;
        private readonly PlayerMovement playerMovement;
        private readonly AudioManager audioManager;
        private readonly GameObject startPlatform;

        [Inject]
        public InitializeState(PlatformManager platformManager, 
            IStateMachine stateMachine, 
            CameraService cameraService,
            PlayerProgress playerProgress,
            IPool crystalPool,
            SpawnerManager spawnerManager,
            PlayerMovement playerMovement,
            AudioManager audioManager)
        {
            this.platformManager = platformManager;
            this.stateMachine = stateMachine;
            this.cameraService = cameraService;
            this.playerProgress = playerProgress;
            this.crystalPool = crystalPool;
            this.spawnerManager = spawnerManager;
            this.playerMovement = playerMovement;
            this.audioManager = audioManager;
        }

        public void Enter()
        {
            playerMovement.enabled = false;
            crystalPool.Initialize();
            cameraService.Initialize();
            spawnerManager.SubscribeToEvent();
            audioManager.SubscribeToEvent();
            platformManager.Initialize();
            playerMovement.ChangeToPlayerMove();
            stateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
        }
    }
}