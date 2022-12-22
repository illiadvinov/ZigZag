using System.Collections.Generic;
using CodeBase.Infrastructure.Assets;
using CodeBase.Infrastructure.Events;
using CodeBase.Platforms;
using UnityEngine;
using Zenject;

namespace CodeBase.Crystals
{
    public class CrystalPool : IPool
    {
        private readonly EventReferer eventReferer;
        private readonly Transform crystalContainer;
        private readonly CrystalFactory crystalFactory;
        private Stack<GameObject> crystals;

        [Inject]
        public CrystalPool(IAssetProvider assetProvider,
            EventReferer eventReferer,
            [Inject(Id = "CrystalsContainer")] Transform crystalContainer)
        {
            this.eventReferer = eventReferer;
            this.crystalContainer = crystalContainer;
            crystalFactory = new CrystalFactory(assetProvider);
            crystals = new Stack<GameObject>();
        }


        public void Initialize()
        {
            for (int i = 0; i < 20; i++)
            {
                GameObject crystal = crystalFactory.CreateCrystal(crystalContainer);
                crystal.GetComponent<CrystalRespawn>().Initialize(this, eventReferer);
                crystal.SetActive(false);
                crystals.Push(crystal);
            }
        }

        public void Add(GameObject objectToAdd)
        {
            crystals.Push(objectToAdd);
        }

        public GameObject Get()
        {
            return crystals.Count > 0 ? crystals.Pop() : null;
        }
    }
}