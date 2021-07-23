using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Dagger : PickupBase
{
    [SerializeField] private float m_damage = 0;
    [SerializeField] private int m_throwForce = 0;
    [SerializeField] private LayerMask m_hitMask = default;
    private IHitable m_owner = null;

    private List<RaycastHit2D> hits = new List<RaycastHit2D>();

    private void Start()
    {
        Init();
        pickupItem = OnPickup;
        CustomThrowAction = OnThrow;
        // TODO add collision dmg dealing
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == m_hitMask)
        {
            Debug.Log("HIT: " + collision.gameObject.name);
            IHitable hitable = collision.gameObject.GetComponent<IHitable>();
            hitable?.Hit(m_damage, m_movement.gameObject, gameObject);
        }
    }

    private void OnPickup()
    {
        m_owner = m_movement.GetComponent<IHitable>();
    }

    private void OnThrow()
    {
        m_owner = null;

        int dir = (int)(m_movement.PlayerDir == PlayerDirection.left ? Direction.Left : Direction.Right);
        Vector2 v2 = new Vector2(dir * m_throwForce, 4);

        Quaternion meh = new Quaternion();
        switch (dir)
        {
            case int x when x < 0:
                {
                    meh.z += 90;
                    break;
                }
            case int x when x > 0:
                {
                    meh.z -= 90;
                    break;
                }
            default:
                break;
        }

        m_RB.AddForce(v2, ForceMode2D.Impulse);
        transform.rotation = Quaternion.identity;
        transform.rotation = meh;
    }
}