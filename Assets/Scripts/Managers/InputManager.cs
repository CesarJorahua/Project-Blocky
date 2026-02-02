using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ProjectBlocky.Managers
{
    /// <summary>
    /// Manages player input for the game, handling both touch and mouse interactions.
    /// Detects clicks/taps on game objects and triggers the appropriate block interactions through raycasting.
    /// </summary>
    ///TODO: Implement input interface for unit testing
    public class InputManager : MonoBehaviour
    {
        private PlayerActions _playerInputActions;
        private GridManager _gridManager;

        [Inject]
        public void Construct(GridManager gridManager)
        {
            _gridManager = gridManager;
        }

        private void Awake()
        {
            _playerInputActions = new PlayerActions();
            if (!_playerInputActions.Game.enabled)
                _playerInputActions.Game.Enable();

        }

        private void OnEnable()
        {
            _playerInputActions.Game.Click.started += OnClickStarted;
        }

        private void OnDisable()
        {
            _playerInputActions.Game.Click.started -= OnClickStarted;
        }


        /// <summary>
        /// Handles click/touch input by performing a raycast to detect block interactions.
        /// Retrieves the screen position and triggers a callback if a Block component is hit.
        /// </summary>
        /// <param name="context">The input action callback context from the input system.</param>
        private void OnClickStarted(InputAction.CallbackContext context)
        {
            Vector2 screenPosition = GetPrimaryPosition();
            Vector2 worldPoint = Camera.main!.ScreenToWorldPoint(screenPosition);
            Collider2D hit = Physics2D.OverlapPoint(worldPoint);

            if (hit != null && hit.TryGetComponent<Block>(out var block))
            {
                _gridManager.Handle(block);
            }
        }

        /// <summary>
        /// Returns the position of the pointer device. Works both for touch and mouse input.
        /// </summary>
        private Vector2 GetPrimaryPosition()
        {
            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
                return Touchscreen.current.primaryTouch.position.ReadValue();

            return Mouse.current.position.ReadValue();
        }
    } 
}
