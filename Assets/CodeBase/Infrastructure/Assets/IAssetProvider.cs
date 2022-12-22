using UnityEngine;

namespace CodeBase.Infrastructure.Assets
{
    public interface IAssetProvider
    {
        GameObject GetFromResources(string path);
    }
}