using System;

namespace ProjectBlocky.Managers
{
    public class MoveManager
    {
        public int Moves { get; private set; }
        
        public event Action<int> OnMovesChanged;
        public event Action OnGameOver;

        public void Initialize()
        {
            Moves = 5;

            OnMovesChanged?.Invoke(Moves);
        }

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