using CodeBase.Player;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI
{
    public class MenuText : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text gameText;
        private PlayerProgress playerProgress;

        [Inject]
        public void Construct(PlayerProgress playerProgress)
        {
            this.playerProgress = playerProgress;
        }

        private void OnEnable()
        {
            scoreText.text = $"Best score - {playerProgress.MaxScore}";
            gameText.text = $"Games played - {playerProgress.PlayedGames}";
        }
    }
}