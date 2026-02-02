using System;

namespace ProjectBlocky.Managers
{
    public class ScoreManager
    {
        public int Score { get; private set; }

        public event Action<int> OnScoreChanged;

        public void Initialize()
        {
            Score = 0;

            OnScoreChanged?.Invoke(Score);
        }
        public void AddScore(int amount)
        {
            Score += amount;
            OnScoreChanged?.Invoke(Score);
        }
    }
}