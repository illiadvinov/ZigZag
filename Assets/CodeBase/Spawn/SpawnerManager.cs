using CodeBase.Crystals;
using CodeBase.Infrastructure.Events;
using CodeBase.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Spawn
{
    public class SpawnerManager
    {
        private readonly PlatformManager platformManager;
        private readonly EventReferer eventReferer;
        private readonly CrystalSpawner crystalManager;
        private readonly GameObject startPlatform;

        [Inject]
        public SpawnerManager(PlatformManager platformManager, EventReferer eventReferer, CrystalSpawner crystalManager,
            [Inject(Id = "StartPlatform")] GameObject startPlatform)
        {
            this.platformManager = platformManager;
            this.eventReferer = eventReferer;
            this.crystalManager = crystalManager;
            this.startPlatform = startPlatform;
        }

        public void SubscribeToEvent()
        {
            eventReferer.OnDeactivateCrystal += crystalManager.DeactivateCrystal;
            eventReferer.OnDeactivatePlatform += ReusePlatform;
        }

        public void UnsubscribeFromEvent()
        {
            eventReferer.OnDeactivateCrystal -= crystalManager.DeactivateCrystal;
            eventReferer.OnDeactivatePlatform -= ReusePlatform;
        }

        public void Spawn()
        {
            startPlatform.SetActive(true);
            platformManager.SetPreviousToFirst();
            for (int i = 0; i < 30; i++)
            {
                Vector3 position = platformManager.Spawn(i);
                int randomNum = Random.Range(0, 5);
                if (randomNum == 0)
                    crystalManager.SpawnCrystal(position);
            }
        }

        public void Reset()
        {
            startPlatform.SetActive(false);
            crystalManager.Reset();
            platformManager.Reset();
        }

        private void ReusePlatform(GameObject platform)
        {
            Vector3 platformPosition = platformManager.Reuse(platform);
            int randomNum = Random.Range(0, 5);
            if (randomNum == 0)
                crystalManager.SpawnCrystal(platformPosition);
        }
    }
}