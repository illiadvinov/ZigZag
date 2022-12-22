using System;
using System.Numerics;
using CodeBase.Infrastructure.Events;
using CodeBase.Infrastructure.Input;
using DG.Tweening;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace CodeBase.Player
{
    public class PlayerMover : Mover
    {
        private readonly EventReferer eventReferer;
        private readonly PlayerMovement playerMovement;
        private readonly float referenceValue;
        private readonly Transform transform;
        private Vector3 direction;

        public PlayerMover(EventReferer eventReferer, PlayerMovement playerMovement, float referenceValue, Transform transform,
            InputService inputService)
        {
            inputService.OnClick += ChangeDirection;
            this.eventReferer = eventReferer;
            this.playerMovement = playerMovement;
            this.referenceValue = referenceValue;
            this.transform = transform;
            direction = Vector3.forward;
        }

        public override void Move(float deltaTime, float speed)
        {
            deltaTime = speed * Time.deltaTime;
            transform.Translate(direction * deltaTime);
            float yThreshold = .05f;
            if (Math.Abs(transform.position.y - referenceValue) > yThreshold)
            {
                eventReferer.PlayerFell();
                playerMovement.enabled = false;
            }
        }

        public override void Reset() =>
            direction = Vector3.forward;

        private void ChangeDirection() => 
            direction = direction == Vector3.forward ? Vector3.left : Vector3.forward;
    }
}