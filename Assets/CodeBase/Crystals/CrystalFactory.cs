using CodeBase.Infrastructure.Assets;
using UnityEngine;

namespace CodeBase.Crystals
{
    public class CrystalFactory
    {
        private const string crystalPath = "Crystal";

        private readonly IAssetProvider assetProvider;

        public CrystalFactory(IAssetProvider assetProvider) =>
            this.assetProvider = assetProvider;

        public GameObject CreateCrystal(Transform parent = null) =>
            Object.Instantiate(assetProvider.GetFromResources(crystalPath), parent);
    }
}