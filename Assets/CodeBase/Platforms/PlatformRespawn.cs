using System;
using System.Collections;
using CodeBase.Infrastructure.Events;
using UnityEngine;

namespace CodeBase.Platforms
{
    public class PlatformRespawn : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigidbody;
        private PlatformManager platformManager;
        private EventReferer eventReferer;

        public void Construct(PlatformManager platformManager, EventReferer eventReferer)
        {
            this.platformManager = platformManager;
            this.eventReferer = eventReferer;
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.CompareTag("Player"))
            {
                rigidbody.isKinematic = false;
                StartCoroutine(DisablePlatform());
            }
        }

        private IEnumerator DisablePlatform()
        {
            yield return new WaitForSeconds(2);
            rigidbody.isKinematic = true;
            eventReferer?.DeactivatePlatform(gameObject);
        }
    }
}