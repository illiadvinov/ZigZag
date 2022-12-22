using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.StateMachine.States
{
    public class FinalState : IState
    {
        private readonly GameObject gameOverCanvas;

        [Inject]
        public FinalState([Inject(Id = "GameOverCanvas")]GameObject gameOverCanvas)
        {
            this.gameOverCanvas = gameOverCanvas;
        }
        
        public void Enter()
        {
            EnableCanvas();
        }

        private async void EnableCanvas()
        {
            await UniTask.Delay(1000);
            gameOverCanvas.SetActive(true);
        }

        public void Exit()
        {
            gameOverCanvas.SetActive(false);
        }
    }
}