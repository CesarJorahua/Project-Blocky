using System;
using UnityEngine;

namespace ProjectBlocky.Managers
{
    /// <summary>
    /// Central game state manager responsible for game flow control, including restart functionality.
    /// </summary>
    public class GameManager
    {
        public event Action OnGameRestart;

        private readonly ScoreManager _scoreManager;
        private readonly MoveManager _moveManager;
        private readonly GridManager _gridManager;

        public GameManager(ScoreManager scoreManager, MoveManager moveManager, GridManager gridManager)
        {
            _scoreManager = scoreManager;
            _moveManager = moveManager;
            _gridManager = gridManager;
        }

        /// <summary>
        /// Restarts the game by reinitializing all managers and triggering restart events.
        /// </summary>
        public void RestartGame()
        {
            Debug.Log("Game Restarting...");
            
            // Clear and reinitialize all managers
            _scoreManager.Initialize();
            _moveManager.Initialize();
            _gridManager.RestartGrid();
            
            // Notify all subscribers that the game has restarted
            OnGameRestart?.Invoke();
        }
    }
}
