using System;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

// TODO make player action to throw weapon away
public class PickupBase : MonoBehaviourPunCallbacks, IPickupAble
{
    private Rigidbody2D m_RB = null;
    private Collider2D m_collider = null;

    private InputManager m_input = null;
    protected PlayerPickup m_pickup = null;

    protected Action<GameObject> useItemAction = null;
    protected Action pickupItem = null;
    protected Action CustomThrowAction = null;

    protected void Init()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_collider = GetPickupCollider;
    }

    // master spawns on init
    // add RPC
    public void PickupItem(Transform parent)
    {
        m_collider.enabled = false;
        m_RB.simulated = false;
        transform.SetParent(parent);
        m_pickup = parent.gameObject.GetComponentInParent<PlayerPickup>();

        m_input = m_pickup.GetComponent<InputManager>();
        m_input.OnItemThrow = OnItemThrow;

        transform.localPosition = Vector3.zero;

        pickupItem?.Invoke();
    }

    public void UseItem(GameObject obj)
    {
        if (useItemAction == null)
        {
            Debug.LogWarning("UseItem not set...");
        }
        else
        {
            useItemAction(obj);
        }
    }

    public void ThrowItem(Vector2 dir, bool enableCollider = false)
    {
        m_input.OnItemThrow = null;
        m_input = null;

        transform.SetParent(null);

        m_RB.simulated = true;
        m_RB.velocity += dir;

        m_pickup.m_heldItem = null;
        m_pickup = null;

        CustomThrowAction?.Invoke();
        m_collider.enabled = enableCollider;
    }

    private void OnItemThrow(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        //obj.control
    }

    private Collider2D GetPickupCollider => new List<Collider2D>(GetComponents<Collider2D>()).Find(x => x.isTrigger == false);
}