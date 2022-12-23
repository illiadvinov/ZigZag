using System.Collections.Generic;
using CodeBase.Infrastructure.Assets;
using CodeBase.Infrastructure.Events;
using UnityEngine;
using Zenject;

namespace CodeBase.Platforms
{
    public class PlatformManager
    {
        private readonly EventReferer eventReferer;
        private readonly Transform platformsContainer;
        private readonly PlatformFactory platformFactory;
        private readonly List<GameObject> platforms;
        private GameObject previousPlatform;
        private readonly GameObject startPlatform;


        [Inject]
        public PlatformManager(IAssetProvider assetProvider,
            EventReferer eventReferer,
            [Inject(Id = "PlatformsContainer")] Transform platformsContainer,
            [Inject(Id = "StartPlatform")] GameObject startPlatform)
        {
            this.eventReferer = eventReferer;
            this.platformsContainer = platformsContainer;
            this.startPlatform = startPlatform;
            platformFactory = new PlatformFactory(assetProvider);
            platforms = new List<GameObject>();
            previousPlatform = startPlatform;
        }

        public void Initialize()
        {
            for (int i = 0; i < 30; i++)
                InstantiatePlatform(i);
        }

        public void Reset()
        {
            foreach (GameObject platform in platforms)
                platform.SetActive(false);
            previousPlatform = startPlatform;
        }

        public Vector3 Spawn(int i)
        {
            platforms[i].SetActive(true);
            return DefinePosition(platforms[i]);
        }

        public Vector3 Reuse(GameObject objectToAdd)
        {
            if (!platforms.Contains(objectToAdd))
                platforms.Add(objectToAdd);

            DefinePosition(objectToAdd);
            objectToAdd.SetActive(true);
            return objectToAdd.transform.position;
        }

        public void SetPreviousToFirst() =>
            previousPlatform = startPlatform;

        public GameObject GetNextPlatform(GameObject platform)
        {
            if (platforms.Contains(platform))
            {
                for (int i = 0; i < platforms.Count; i++)
                {
                    if (i == platforms.Count - 1)
                        return platforms[0];
                    if (platforms[i] == platform)
                        return platforms[++i];
                }
            }

            return platforms[0];
        }

        private Vector3 DefinePosition(GameObject platform)
        {
            int randomNumber = Random.Range(0, 2);
            Transform placePosition = previousPlatform.GetComponent<PlatformInfo>().PlacesPositions[randomNumber];

            platform.transform.position = placePosition.localPosition * platform.transform.localScale.x + placePosition.position;
            previousPlatform = platform;
            return platform.transform.position;
        }

        private void InstantiatePlatform(int platformIndex)
        {
            GameObject platform = platformFactory.CreatePlatform(platformsContainer);
            platform.GetComponent<PlatformRespawn>().Construct(this, eventReferer);
            platform.name = platform.name.Replace("(Clone)", $"{platformIndex.ToString()}");
            platforms.Add(platform);
        }
    }
}