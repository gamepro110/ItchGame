using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerPickup : MonoBehaviourPunCallbacks
{
    private InputManager m_input = null;

    private Vector2 m_castSize = Vector2.zero;
    [SerializeField] private RaycastHit2D m_hit = default;
    [SerializeField] private IPickupAble m_pickupable = null;
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
        if (m_heldItem == null)
        {
            m_hit = Physics2D.BoxCast(transform.position, m_castSize, 0, Vector2.zero, 0, m_layers);
            if (m_hit.transform != null)
            {
                m_pickupable = m_hit.transform.GetComponent<IPickupAble>();

                if (m_pickupable != null)
                {
                    //m_pickupable.PickupItem(m_pickupParent);
                    //m_heldItem = m_pickupable;
                    PhotonView view = m_hit.transform.GetComponent<PickupBase>().photonView;

                    photonView.RPC("RPCPickupItem", RpcTarget.All, view.ViewID);
                }
            }
        }
        else
        {
            int id = m_pickupParent.GetChild(0).GetComponent<PhotonView>().ViewID;
            photonView.RPC("RPCThrowItem", RpcTarget.All, id);
        }
    }

    [PunRPC]
    public void RPCPickupItem(int id, PhotonMessageInfo info)
    {
        GameObject _pickup = new List<PhotonView>(FindObjectsOfType<PhotonView>()).Find(x => x.ViewID == id).gameObject;

        m_pickupable = _pickup.GetComponent<IPickupAble>();
        m_pickupable.PickupItem(m_pickupParent);
        m_heldItem = _pickup.GetComponent<IPickupAble>();
    }

    [PunRPC]
    public void RPCThrowItem(int id, PhotonMessageInfo info)
    {
        GameObject _pickup = new List<PhotonView>(FindObjectsOfType<PhotonView>()).Find(x => x.ViewID == id).gameObject;

        m_pickupable = _pickup.GetComponent<IPickupAble>();
        m_pickupable.ThrowItem(Vector2.up, true);
        m_heldItem = null;
    }
}