using System;
using CodeBase.Infrastructure.Events;
using CodeBase.Infrastructure.Input;
using CodeBase.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 10f;
        [SerializeField] private Transform previousPlatform;
        private float referenceValue;
        private float yThreshold;
        private Mover mover;
        private AutoMover autoMover;
        private PlayerMover playerMover;

        public bool IsAuto { get; private set; }

        [Inject]
        public void Construct(InputService inputService, EventReferer eventReferer, PlatformManager platformManager)
        {
            autoMover = new AutoMover(previousPlatform, platformManager, transform);
            playerMover = new PlayerMover(eventReferer, this, transform.position.y, transform, inputService);
        }

        public void ChangeToAutoMove()
        {
            mover = autoMover;
            IsAuto = true;
        }

        public void ChangeToPlayerMove()
        {
            mover = playerMover;
            IsAuto = false;
        }

        public void Reset()
        {
            transform.position = new Vector3(0, 1, -10);
            mover.Reset();
            enabled = false;
        }

        private void Update()
        {
            mover.Move(Time.deltaTime, speed);
        }
    }
}