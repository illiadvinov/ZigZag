using CodeBase.Infrastructure.Events;
using CodeBase.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Audio
{
    public class AudioManager
    {
        
        private readonly EventReferer eventReferer;
        private readonly InputService inputService;
        private readonly AudioSource gameOverAs;
        private readonly AudioSource clickAs;
        private readonly AudioSource crystalAs;

        [Inject]
        public AudioManager(EventReferer eventReferer,
            InputService inputService,
            [Inject(Id = "GameOverAudioSource")] AudioSource gameOverAS,
            [Inject(Id = "ClickAudioSource")] AudioSource clickAS,
            [Inject(Id = "CrystalAudioSource")] AudioSource crystalAS)
        {
            this.eventReferer = eventReferer;
            this.inputService = inputService;
            gameOverAs = gameOverAS;
            clickAs = clickAS;
            crystalAs = crystalAS;
        }

        public void SubscribeToEvent()
        {
            eventReferer.OnPlayerFell += PlayGameOverSound;
            eventReferer.OnDeactivateCrystal += PlayCrystalSound;
            inputService.OnClick += PlayClickSound;
        }
        public void UnsubscribeFromEvent()
        {
            eventReferer.OnPlayerFell -= PlayGameOverSound;
            eventReferer.OnDeactivateCrystal -= PlayCrystalSound;
            inputService.OnClick -= PlayClickSound;
        }

        private void PlayClickSound()
        {
            clickAs.Play();
        }

        private void PlayCrystalSound(GameObject obj)
        {
            crystalAs.PlayOneShot(crystalAs.clip);
        }

        private void PlayGameOverSound()
        {
            gameOverAs.Play();
        }
    }
}