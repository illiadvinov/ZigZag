using System;
using System.Collections;
using CodeBase.Infrastructure.Events;
using CodeBase.Platforms;
using UnityEngine;

namespace CodeBase.Crystals
{
    public class CrystalRespawn : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        private IPool crystalPool;
        private EventReferer eventReferer;

        public void Initialize(IPool crystalPool, EventReferer eventReferer)
        {
            this.crystalPool = crystalPool;
            this.eventReferer = eventReferer;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) 
                particles.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player")) 
                eventReferer.DeactivateCrystal(gameObject);
        }

        private IEnumerator DeactivateCrystal()
        {
            yield return new WaitForSeconds(1);
            crystalPool.Add(gameObject);
            gameObject.SetActive(false);
        }
    }
}