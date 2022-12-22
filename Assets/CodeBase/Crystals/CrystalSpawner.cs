using CodeBase.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Crystals
{
    public class CrystalSpawner
    {
        private readonly IPool crystalPool;
        private readonly Transform crystalContainer;

        [Inject]
        public CrystalSpawner(IPool crystalPool,
            [Inject(Id = "CrystalsContainer")] Transform crystalContainer)
        {
            this.crystalPool = crystalPool;
            this.crystalContainer = crystalContainer;
        }

        public void SpawnCrystal(Vector3 position)
        {
            GameObject crystal = crystalPool.Get();
            if (crystal == null)
                return;
            crystal.transform.position = new Vector3(position.x, crystal.transform.position.y, position.z);
            crystal.SetActive(true);
        }

        public void DeactivateCrystal(GameObject crystal)
        {
            crystal.SetActive(false);
            crystalPool.Add(crystal);
        }

        public void Reset()
        {
            for (int i = 0; i < crystalContainer.childCount; i++)
            {
                Transform crystal = crystalContainer.GetChild(i);
                if (crystal.gameObject.activeInHierarchy)
                    DeactivateCrystal(crystal.gameObject);
            }
        }
    }
}