using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerPickup))]
public class PlayerUseItem : MonoBehaviourPunCallbacks
{
    private InputManager m_input = null;
    private PlayerPickup m_pickup = null;

    private void Start()
    {
        if (photonView.IsMine)
        {
            m_input = FindObjectOfType<InputManager>();
            m_pickup = GetComponent<PlayerPickup>();

            m_input.OnUseItem = UseItem;
        }
    }

    private void UseItem(InputAction.CallbackContext obj)
    {
        m_pickup.m_heldItem?.UseItem(gameObject);
    }
}