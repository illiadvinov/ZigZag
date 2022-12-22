using CodeBase.Infrastructure.Assets;
using UnityEngine;

namespace CodeBase.Platforms
{
    public class PlatformFactory
    {
        private readonly IAssetProvider assetProvider;
        private const string platformPath = "Platform";

        public PlatformFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }

        public GameObject CreatePlatform(Transform platformsContainer = null)
        {
            GameObject platform = Object.Instantiate(assetProvider.GetFromResources(platformPath), platformsContainer);
            return platform;
        }
    }
}