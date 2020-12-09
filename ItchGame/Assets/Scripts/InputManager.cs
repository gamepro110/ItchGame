using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private GameControls m_controls = null;

    internal Vector2 LeftStick { get => m_controls.Player.Movement.ReadValue<Vector2>(); }
    //internal bool Pickup_UseItem { get => m_controls.Player.Pickup_UseItem.ReadValue<float>() > 0; }

    internal Action<UnityEngine.InputSystem.InputAction.CallbackContext> jump_started;
    internal Action<UnityEngine.InputSystem.InputAction.CallbackContext> jump_canceled;
    internal Action<UnityEngine.InputSystem.InputAction.CallbackContext> OnPickup;
    internal Action<UnityEngine.InputSystem.InputAction.CallbackContext> OnUseItem;

    private void Awake()
    {
        m_controls = new GameControls();
        m_controls.Player.Jump.started += JumpStarted;
        m_controls.Player.Jump.canceled += JumpCanceled;
        m_controls.Player.PickupItem.performed += PickupItem;
        m_controls.Player.UseItem.performed += UseHeldItem;
    }

    private void JumpStarted(InputAction.CallbackContext obj) => jump_started(obj);

    private void JumpCanceled(InputAction.CallbackContext obj) => jump_canceled(obj);

    private void PickupItem(InputAction.CallbackContext obj) => OnPickup(obj);

    private void UseHeldItem(InputAction.CallbackContext obj) => OnUseItem(obj);

    private void OnEnable()
    {
        m_controls.Enable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }
}