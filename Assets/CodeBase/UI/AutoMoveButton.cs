using CodeBase.Infrastructure.Input;
using CodeBase.Player;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class AutoMoveButton : MonoBehaviour
    {
        private PlayerMovement playerMovement;
        private bool isTicked;
        private InputService inputService;

        [Inject]
        public void Construct(PlayerMovement playerMovement, InputService inputService)
        {
            this.playerMovement = playerMovement;
            this.inputService = inputService;
        }

        public void Click()
        {
            if (!isTicked)
            {
                playerMovement.ChangeToAutoMove();
                isTicked = false;
            }
            else
            {
                playerMovement.ChangeToPlayerMove();
                isTicked = true;
            }
        }
    }
}