using Photon.Pun;
using UnityEngine;

public class HealBlob : PickupBase
{
    [SerializeField] private float m_healAmount = 10;

    private void Start()
    {
        Init();
        useItemAction = UsingItem;
    }

    private void UsingItem(GameObject obj)
    {
        obj.GetComponent<PlayerDamagable>().Heal(m_healAmount);
        m_pickup.m_heldItem = null;
        transform.SetParent(null);
        PhotonNetwork.Destroy(gameObject);
    }
}