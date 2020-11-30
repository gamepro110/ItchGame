using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBlob : MonoBehaviour, IPickupAble
{
    [SerializeField] private float m_healAmount = 10;

    public void PickupItem()
    { return; }

    public void UseItem(GameObject obj)
    {
        obj.GetComponent<PlayerDamagable>().Hit(-m_healAmount, true);
        PhotonNetwork.Destroy(gameObject);
    }
}