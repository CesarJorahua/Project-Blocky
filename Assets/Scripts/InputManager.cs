using UnityEngine;
using UnityEngine.InputSystem;

///TODO: Implement input interface for unit testing
/// <summary>
/// Manages player input for the game, handling both touch and mouse interactions.
/// Detects clicks/taps on game objects and triggers the appropriate block interactions through raycasting.
/// </summary>
public class InputManager : MonoBehaviour
{
    private PlayerActions _playerInputActions;

    private void Awake()
    {
        _playerInputActions = new PlayerActions();
        if(!_playerInputActions.Game.enabled)
            _playerInputActions.Game.Enable();
    }
    private void OnEnable()
    {
        _playerInputActions.Game.Click.started += PlayerInput_onActionTriggered;
    }

    private void OnDisable()
    {
        _playerInputActions.Game.Click.started -= PlayerInput_onActionTriggered;
    }


    /// <summary>
    /// Handles click/touch input by performing a raycast to detect block interactions.
    /// Retrieves the screen position and triggers a callback if a Block component is hit.
    /// </summary>
    /// <param name="context">The input action callback context from the input system.</param>
    private void PlayerInput_onActionTriggered(InputAction.CallbackContext context)
    {
        Vector2 screenPosition = GetPrimaryPosition();
        Debug.Log($"[{GetType()}] Current cluck/touch position "+ screenPosition);
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Vector2 origin = ray.origin;
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log($"[{GetType()}] Clicked on: " + hit.collider.gameObject.name);

            if (hit.collider.TryGetComponent<Block>(out var block))
            {
                block.OnClickedBlock();
            }
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
