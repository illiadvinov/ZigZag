using System;
using CodeBase.Platforms;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Player
{
    public class AutoMover : Mover
    {
        private readonly PlatformManager platformManager;
        private readonly Transform transform;
        private Transform previousPlatform;
        private bool isPlaying;

        public AutoMover(Transform previousPlatform, PlatformManager platformManager, Transform transform)
        {
            this.previousPlatform = previousPlatform;
            this.platformManager = platformManager;
            this.transform = transform;
        }

        public override void Move(float deltaTime, float speed)
        {
            if (isPlaying)
                return;
            
            isPlaying = true;
            transform.DOMove(new Vector3(previousPlatform.position.x, transform.position.y, previousPlatform.position.z), .3f)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    isPlaying = false;
                    previousPlatform = platformManager.GetNextPlatform(previousPlatform.gameObject).transform;
                });
        }

        public override void Reset()
        {
            isPlaying = false;
        }
    }
}