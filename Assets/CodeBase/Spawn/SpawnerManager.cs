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

        [Inject]
        public SpawnerManager(PlatformManager platformManager, EventReferer eventReferer, CrystalSpawner crystalManager)
        {
            this.platformManager = platformManager;
            this.eventReferer = eventReferer;
            this.crystalManager = crystalManager;
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
            crystalManager.Reset();
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