using CodeBase.Infrastructure.Events;
using CodeBase.Infrastructure.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Player
{
    public class PlayerProgress
    {
        private readonly InputService inputService;
        private readonly EventReferer eventReferer;
        public int Score { get; private set; }
        public int MaxScore { get; private set; }
        public int CrystalScore { get; private set; }
        public int PlayedGames { get; private set; }

        [Inject]
        public PlayerProgress(InputService inputService, EventReferer eventReferer)
        {
            this.inputService = inputService;
            this.eventReferer = eventReferer;
        }

        public void ResetCurrentScore() =>
            Score = 0;

        public void UpdateCrystalProgress() =>
            CrystalScore++;

        public void UpdateProgress()
        {
            Score++;
            if (Score > MaxScore)
                MaxScore = Score;
        }

        public void UpdatePlayedGames() =>
            PlayedGames++;
    }
}