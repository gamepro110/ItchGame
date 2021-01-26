using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;

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
                    m_pickupable.PickupItem(m_pickupParent);
                    m_heldItem = m_pickupable;

                    photonView.RPC("RPCPickupItem", RpcTarget.All);
                }
            }
        }
        else
        {
            //Debug.LogWarning("YEETT", this);
            //m_input.OnItemThrow(obj);
        }
    }

    [PunRPC]
    public void RPCPickupItem(PhotonMessageInfo _info) // TODO TEST RPC CALL
    {
        PlayerPickup _pickup = _info.photonView.GetComponent<PlayerPickup>();
        Debug.LogWarning(_pickup.gameObject.name);
        _pickup.m_heldItem.PickupItem(_pickup.m_pickupParent);
        //_pickup.PickupItem(_pickup.m_pickupParent);
    }
}