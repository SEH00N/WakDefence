using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

[CreateAssetMenu(menuName = "SO/Input/InputReader")]
public class InputReaderSO : ScriptableObject, IPlayerActions
{
    private Controls controls = null;

    private Vector2 mousePosition = Vector2.zero;
    public Vector2 MousePosition => mousePosition;

    public event Action OnLeftClickEvent = null;
    public event Action OnRightClickEvent = null;

    private void OnEnable()
    {
        if(controls == null)
            controls = new Controls();
        
        controls.Player.AddCallbacks(this);
        controls.Enable();
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnLeftClickEvent?.Invoke();
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        if(context.performed)
            OnRightClickEvent?.Invoke();
    }

    public void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePosition = context.ReadValue<Vector2>();
    }
}
