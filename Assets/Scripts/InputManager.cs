using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerActions _playerInpoutActions;

    private void Awake()
    {
        _playerInpoutActions = new PlayerActions();
        if(!_playerInpoutActions.Game.enabled)
            _playerInpoutActions.Game.Enable();
    }
    private void OnEnable()
    {
        _playerInpoutActions.Game.Click.started += PlayerInput_onActionTriggered;
    }

    private void OnDisable()
    {
        _playerInpoutActions.Game.Click.started -= PlayerInput_onActionTriggered;
    }

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
                // grid.OnBlockClicked(block);
            }
        }
    }

    private Vector2 GetPrimaryPosition()
    {
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            return Touchscreen.current.primaryTouch.position.ReadValue();

        return Mouse.current.position.ReadValue();
    }
}
