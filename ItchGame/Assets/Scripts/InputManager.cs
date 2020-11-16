using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private GameControls m_controls = null;

    internal Vector2 LeftStick { get => m_controls.Player.Movement.ReadValue<Vector2>(); }

    internal Action<UnityEngine.InputSystem.InputAction.CallbackContext> jump;

    private void Awake()
    {
        m_controls = new GameControls();
        m_controls.Player.Jump.performed += Jump_performed;
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        jump(obj);
    }

    private void OnEnable()
    {
        m_controls.Enable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }
}