using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    private GameControls m_controls = null;

    internal Vector2 LeftStick { get => m_controls.Player.Movement.ReadValue<Vector2>(); }

    internal Action<UnityEngine.InputSystem.InputAction.CallbackContext> jump_started;
    internal Action<UnityEngine.InputSystem.InputAction.CallbackContext> jump_canceled;

    private void Awake()
    {
        m_controls = new GameControls();
        m_controls.Player.Jump.started += Jump_started;
        m_controls.Player.Jump.canceled += Jump_canceled;
    }

    private void Jump_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        jump_started(obj);
    }

    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        jump_canceled(obj);
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