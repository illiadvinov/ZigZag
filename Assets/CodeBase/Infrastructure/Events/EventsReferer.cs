using System;
using UnityEngine;

namespace CodeBase.Infrastructure.Events
{
    public class EventReferer
    {
        public event Action OnPlayerFell;
        public event Action<GameObject> OnDeactivateCrystal;
        public event Action<GameObject> OnDeactivatePlatform;

        public void PlayerFell() =>
            OnPlayerFell?.Invoke();

        public void DeactivateCrystal(GameObject crystal) =>
            OnDeactivateCrystal?.Invoke(crystal);
        
        public void DeactivatePlatform(GameObject platform) =>
            OnDeactivatePlatform?.Invoke(platform);
    }
}