using System;

namespace ProjectBlocky.Managers
{
    /// <summary>
    /// Manages the player's score.
    /// </summary>
    public class ScoreManager
    {
        public int Score { get; private set; }

        public event Action<int> OnScoreChanged;

        /// <summary>
        /// Initializes the score manager.
        /// </summary>
        public void Initialize()
        {
            Score = 0;
            OnScoreChanged?.Invoke(Score);
        }

        /// <summary>
        /// Adds a score to the current score.
        /// </summary>
        public void AddScore(int amount)
        {
            Score += amount;
            OnScoreChanged?.Invoke(Score);
        }
    }
}