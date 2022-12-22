using CodeBase.Camera;
using CodeBase.Player;
using CodeBase.Spawn;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly GameObject mainMenuCanvas;
        private readonly GameObject startPlatform;
        private readonly SpawnerManager spawnerManager;
        private readonly PlayerMovement playerMovement;
        private readonly CameraService cameraService;
        private readonly PlayerProgress playerProgress;

        [Inject]
        public MenuState(SpawnerManager spawnerManager, PlayerMovement playerMovement,
            CameraService cameraService,
            PlayerProgress playerProgress,
            [Inject(Id = "MainMenuCanvas")] GameObject mainMenuCanvas,
            [Inject(Id = "StartPlatform")] GameObject startPlatform)
        {
            this.mainMenuCanvas = mainMenuCanvas;
            this.startPlatform = startPlatform;
            this.spawnerManager = spawnerManager;
            this.playerMovement = playerMovement;
            this.cameraService = cameraService;
            this.playerProgress = playerProgress;
        }

        public void Enter()
        {
            playerProgress.ResetCurrentScore();
            playerMovement.Reset();
            cameraService.Reset();
            spawnerManager.Reset();
            startPlatform.transform.position = new Vector3(0, -1.5f, -3);
            spawnerManager.Spawn();
            mainMenuCanvas.SetActive(true);
        }

        public void Exit()
        {
            mainMenuCanvas.SetActive(false);
        }
    }
}