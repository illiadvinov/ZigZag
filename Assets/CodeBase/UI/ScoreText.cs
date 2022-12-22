using CodeBase.Infrastructure.Events;
using CodeBase.Infrastructure.Input;
using CodeBase.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class ScoreText : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text crystalScoreText;
        private InputService inputService;
        private EventReferer eventReferer;
        private PlayerProgress playerProgress;

        [Inject]
        public void Construct(InputService inputService, EventReferer eventReferer, PlayerProgress playerProgress)
        {
            this.inputService = inputService;
            this.eventReferer = eventReferer;
            this.playerProgress = playerProgress;
        }

        public void Start()
        {
            inputService.OnClick += UpdateProgress;
            eventReferer.OnDeactivateCrystal += UpdateCrystalScore;
        }

        public void OnDestroy()
        {
            inputService.OnClick -= UpdateProgress;
            eventReferer.OnDeactivateCrystal -= UpdateCrystalScore;
        }

        private void UpdateCrystalScore(GameObject obj)
        {
            playerProgress.UpdateCrystalProgress();
            crystalScoreText.text = $"x{playerProgress.CrystalScore.ToString()}";
        }

        private void UpdateProgress()
        {
            playerProgress.UpdateProgress();
            scoreText.text = playerProgress.Score.ToString();
        }
    }
}