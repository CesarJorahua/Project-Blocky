using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace ProjectBlocky.Managers
{
    /// <summary>
    /// Central game state manager responsible for game flow control, including restart functionality.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private ScoreManager _scoreManager;
        private MoveManager _moveManager;
        private GridManager _gridManager;

        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Button restartButton;

        [Inject]
        public void Construct(
            ScoreManager scoreManager,
            MoveManager moveManager,
            GridManager gridManager)
        {
            _scoreManager = scoreManager;
            _moveManager = moveManager;
            _gridManager = gridManager;
        }

        private void Awake()
        {
            // Bind the restart button click to the GameManager's restart method
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartButtonClicked);
            }
        }
        private void OnDestroy()
        {
            // Clean up button listener to prevent memory leaks
            if (restartButton != null)
            {
                restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            }
        }

        private void OnRestartButtonClicked()
        {
            RestartGame();
        }

        private void OnEnable()
        {
            _moveManager!.OnGameOver += ShowGameOver;
        }

        private void OnDisable()
        {
            _moveManager!.OnGameOver -= ShowGameOver;
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
            HideGameOver();
        }

        private void ShowGameOver()
        {
            gameOverScreen.SetActive(true);
        }

        private void HideGameOver()
        {
            gameOverScreen.SetActive(false);
        }
    }
}
