using System;
using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PickupBase : MonoBehaviourPunCallbacks, IPickupAble
{
    private Rigidbody2D m_RB = null;
    private Collider2D m_collider = null;

    protected PlayerPickup m_pickup = null;

    protected Action<GameObject> useItemAction = null;
    protected Action pickupItem = null;
    protected Action CustomThrowAction = null;

    protected void Init()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_collider = GetPickupCollider;
    }

    public void PickupItem(Transform parent)
    {
        m_collider.enabled = false;
        m_RB.simulated = false;
        transform.SetParent(parent);
        m_pickup = parent.gameObject.GetComponentInParent<PlayerPickup>();
        transform.localPosition = Vector3.zero;

        if (pickupItem != null)
        {
            pickupItem();
        }
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

    public void ThrowItem(Vector2 dir)
    {
        transform.SetParent(null);

        m_RB.simulated = true;
        m_RB.velocity += dir;

        m_pickup.m_heldItem = null;
        m_pickup = null;

        CustomThrowAction?.Invoke();
        //m_collider.enabled = true;
    }

    private Collider2D GetPickupCollider
    {
        get
        {
            return new List<Collider2D>(GetComponents<Collider2D>()).Find(x => x.isTrigger == false);
        }
    }
}