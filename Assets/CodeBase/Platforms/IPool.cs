using UnityEngine;

namespace CodeBase.Platforms
{
    public interface IPool
    {
        void Initialize();
        void Add(GameObject objectToAdd);
        GameObject Get();
    }
}