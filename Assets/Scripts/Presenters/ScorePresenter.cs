using TMPro;
using UnityEngine;
using Zenject;
using ProjectBlocky.Managers;

namespace ProjectBlocky.Presenters
{
    /// <summary>
    /// Updates the score text with the current score.
    /// </summary>
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private ScoreManager _manager;

        [Inject]
        public void Construct(ScoreManager manager)
        {
            _manager = manager;
        }

        private void OnEnable()
        {
            _manager.OnScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            _manager.OnScoreChanged -= UpdateScore;
        }

        private void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}