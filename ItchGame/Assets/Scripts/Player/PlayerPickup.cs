using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    private Collider2D m_coll = null;
    [SerializeField] private RaycastHit2D m_hit = default;
    private IPickupAble m_pickup = null;
    [SerializeField] private LayerMask m_layers = default;

    private void Start()
    {
        m_coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        m_hit = Physics2D.BoxCast(transform.position, m_coll.bounds.size, 0, Vector2.zero, 0, m_layers);
        if (m_hit.transform != null)
        {
            m_pickup = m_hit.transform.GetComponent<IPickupAble>();

            m_pickup?.PickupItem();
            m_pickup?.UseItem(gameObject);
        }
    }
}