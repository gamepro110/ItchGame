﻿using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerPickup : PickupBase
{
    private InputManager m_input = null;

    private Vector2 m_castSize = Vector2.zero;
    [SerializeField] private RaycastHit2D m_hit = default;
    private IPickupAble m_pickupable = null;
    [SerializeField] private LayerMask m_layers = default;
    [SerializeField] private Transform m_pickupParent = null;
    [SerializeField] internal IPickupAble m_heldItem = null;

    private void Start()
    {
        if (photonView.IsMine)
        {
            m_input = FindObjectOfType<InputManager>();
            m_castSize = GetComponent<Collider2D>().bounds.size;

            m_input.OnPickup += Pickup;
        }
    }

    private void Pickup(InputAction.CallbackContext obj)
    {
        m_hit = Physics2D.BoxCast(transform.position, m_castSize, 0, Vector2.zero, 0, m_layers);
        if (m_hit.transform != null)
        {
            if (m_heldItem == null)
            {
                m_pickupable = m_hit.transform.GetComponent<IPickupAble>();

                if (m_pickupable != null)
                {
                    m_pickupable.PickupItem(m_pickupParent);
                    m_heldItem = m_pickupable;
                }
            }
        }
    }
}