using System;
using CodeBase.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text maxScoreText;
        private PlayerProgress playerProgress;

        [Inject]
        public void Construct(PlayerProgress playerProgress)
        {
            this.playerProgress = playerProgress;
        }

        private void OnEnable()
        {
            scoreText.text = $"Your score - {playerProgress.Score}";
            maxScoreText.text = $"Your maximum score - {playerProgress.MaxScore}";
        }
    }
}