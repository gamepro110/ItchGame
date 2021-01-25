using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private GameControls m_controls = null;

    internal Vector2 LeftStick { get => m_controls.Player.Movement.ReadValue<Vector2>(); }
    //internal bool Pickup_UseItem { get => m_controls.Player.Pickup_UseItem.ReadValue<float>() > 0; }

    internal Action<InputAction.CallbackContext> jump_started;
    internal Action<InputAction.CallbackContext> jump_canceled;
    internal Action<InputAction.CallbackContext> OnPickup;
    internal Action<InputAction.CallbackContext> OnUseItemStarted;
    internal Action<InputAction.CallbackContext> OnUseItemEnded;
    internal Action<InputAction.CallbackContext> OnItemThrow;
    internal Action<InputAction.CallbackContext> TempYeet;

    private void Awake()
    {
        m_controls = new GameControls();
        m_controls.Player.Jump.started += JumpStarted;
        m_controls.Player.Jump.canceled += JumpCanceled;
        m_controls.Player.PickupItem.started += PickupItem;
        m_controls.Player.UseItem.started += UseHeldItemStart;
        m_controls.Player.UseItem.canceled += UseHeldItemEnd;
        //TODO add to controls
        //m_controls.Player.ThrowItem.performed += UseItemThrow;

        //m_controls.Player.Attack.performed += TempYeeting;
    }

    private void JumpStarted(InputAction.CallbackContext obj) => jump_started(obj);

    private void JumpCanceled(InputAction.CallbackContext obj) => jump_canceled(obj);

    private void PickupItem(InputAction.CallbackContext obj) => OnPickup(obj);

    private void UseHeldItemStart(InputAction.CallbackContext obj) => OnUseItemStarted(obj);

    private void UseHeldItemEnd(InputAction.CallbackContext obj) => OnUseItemEnded(obj);

    private void UseItemThrow(InputAction.CallbackContext obj) => OnItemThrow(obj);

    //private void TempYeeting(InputAction.CallbackContext obj) => TempYeet(obj);

    private void OnEnable()
    {
        m_controls.Enable();
    }

    private void OnDisable()
    {
        m_controls.Disable();
    }
}