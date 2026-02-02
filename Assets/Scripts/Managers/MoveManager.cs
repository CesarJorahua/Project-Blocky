using System;

namespace ProjectBlocky.Managers
{
    /// <summary>
    /// Manages the number of moves the player has.
    /// </summary>
    public class MoveManager
    {
        public int Moves { get; private set; }
        
        public event Action<int> OnMovesChanged;
        public event Action OnGameOver;

        /// <summary>
        /// Initializes the move manager.
        /// </summary>
        public void Initialize()
        {
            Moves = 5;
            OnMovesChanged?.Invoke(Moves);
        }

        /// <summary>
        /// Uses a move.
        /// </summary>
        public void UseMove()
        {
            if (Moves <= 0)
                return;

            Moves--;
            OnMovesChanged?.Invoke(Moves);

            if (Moves == 0)
                OnGameOver?.Invoke();
        }
    }
}