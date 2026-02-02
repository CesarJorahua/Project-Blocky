using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ProjectBlocky.Managers;

namespace ProjectBlocky.Presenters
{
    public class MovePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text movesText;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private Button restartButton;

        private MoveManager _moveManager;
        private GameManager _gameManager;

        [Inject]
        public void Construct(MoveManager moveManager, GameManager gameManager)
        {
            _moveManager = moveManager;
            _gameManager = gameManager;
        }

        private void Awake()
        {
            // Bind the restart button click to the GameManager's restart method
            if (restartButton != null)
            {
                restartButton.onClick.AddListener(OnRestartButtonClicked);
            }
        }

        private void OnEnable()
        {
            _moveManager.OnMovesChanged += UpdateMoves;
            _moveManager.OnGameOver += ShowGameOver;
            _gameManager.OnGameRestart += HideGameOver;
        }

        private void OnDisable()
        {
            _moveManager.OnMovesChanged -= UpdateMoves;
            _moveManager.OnGameOver -= ShowGameOver;
            _gameManager.OnGameRestart -= HideGameOver;
        }

        private void OnDestroy()
        {
            // Clean up button listener to prevent memory leaks
            if (restartButton != null)
            {
                restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            }
        }

        private void UpdateMoves(int moves)
        {
            movesText.text = moves.ToString();
        }

        private void ShowGameOver()
        {
            gameOverScreen.SetActive(true);
        }

        private void HideGameOver()
        {
            gameOverScreen.SetActive(false);
        }

        private void OnRestartButtonClicked()
        {
            _gameManager.RestartGame();
        }
    }
}