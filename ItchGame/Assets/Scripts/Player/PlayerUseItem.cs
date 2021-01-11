using Photon.Pun;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerPickup))]
public class PlayerUseItem : MonoBehaviourPunCallbacks
{
    private InputManager m_input = null;
    private PlayerPickup m_pickup = null;

    private bool m_usePickedupItem = false;

    private void Start()
    {
        if (photonView.IsMine)
        {
            m_input = FindObjectOfType<InputManager>();
            m_pickup = GetComponent<PlayerPickup>();

            m_input.OnUseItemStarted = StartUseItem;
            m_input.OnUseItemEnded = EndUseItem;
        }
    }

    private void Update()
    {
        if (m_usePickedupItem)
        {
            UseItem();
        }
    }

    private void StartUseItem(InputAction.CallbackContext obj)
    {
        m_usePickedupItem = true;
    }

    private void EndUseItem(InputAction.CallbackContext obj)
    {
        m_usePickedupItem = false;
    }

    private void UseItem()
    {
        m_pickup.m_heldItem?.UseItem(gameObject);
    }
}