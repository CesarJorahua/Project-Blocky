using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ProjectBlocky.Managers;

namespace ProjectBlocky.Presenters
{
    /// <summary>
    /// Updates the moves text with the current number of moves.
    /// </summary>
    public class MovePresenter : MonoBehaviour
    {
        [SerializeField] private TMP_Text movesText;

        private MoveManager _moveManager;

        [Inject]
        public void Construct(MoveManager moveManager)
        {
            _moveManager = moveManager;
        }

        private void OnEnable()
        {
            _moveManager.OnMovesChanged += UpdateMoves;
        }

        private void OnDisable()
        {
            _moveManager.OnMovesChanged -= UpdateMoves;
        }

        private void UpdateMoves(int moves)
        {
            movesText.text = moves.ToString();
        }
    }
}