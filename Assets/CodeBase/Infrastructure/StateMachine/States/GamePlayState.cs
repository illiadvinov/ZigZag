using CodeBase.Camera;
using CodeBase.Infrastructure.Events;
using CodeBase.Infrastructure.Input;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class GamePlayState : IState
    {
        private readonly InputService inputService;
        private readonly CameraService cameraService;
        private readonly PlayerMovement playerMovement;
        private readonly PlayerProgress playerProgress;
        private readonly GameObject scoreCanvas;

        [Inject]
        public GamePlayState(IStateMachine stateMachine,
            InputService inputService,
            CameraService cameraService,
            EventReferer eventReferer,
            PlayerMovement playerMovement,
            PlayerProgress playerProgress,
            [Inject(Id = "ScoreCanvas")] GameObject scoreCanvas)
        {
            this.inputService = inputService;
            this.cameraService = cameraService;
            this.playerMovement = playerMovement;
            this.playerProgress = playerProgress;
            this.scoreCanvas = scoreCanvas;
            eventReferer.OnPlayerFell += stateMachine.Enter<FinalState>;
        }

        public void Enter()
        {
            playerProgress.UpdatePlayedGames();
            playerMovement.enabled = true;
            scoreCanvas.SetActive(true);
            cameraService.StartCameraMovement();
            if (!playerMovement.IsAuto)
                inputService.StartInput();
        }

        public void Exit()
        {
            inputService.StopInput();
            scoreCanvas.SetActive(false);
            cameraService.StopCameraMovement();
        }
    }
}