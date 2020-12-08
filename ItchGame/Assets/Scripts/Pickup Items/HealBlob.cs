using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealBlob : MonoBehaviour, IPickupAble
{
    [SerializeField] private float m_healAmount = 10;
    private Rigidbody2D m_RB = null;
    private Collider2D m_col = null;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_col = GetComponent<Collider2D>();
    }

    public void PickupItem(Transform parent)
    {
        m_col.enabled = false;
        m_RB.simulated = false;
        transform.SetParent(parent);
    }

    public void UseItem(GameObject obj)
    {
        obj.GetComponent<PlayerDamagable>().Hit(-m_healAmount, true);

        PhotonNetwork.Destroy(gameObject);
    }
}