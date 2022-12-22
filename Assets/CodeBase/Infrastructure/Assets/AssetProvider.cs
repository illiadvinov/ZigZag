using UnityEngine;

namespace CodeBase.Infrastructure.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject GetFromResources(string path) =>
            Resources.Load<GameObject>(path);
    }
}