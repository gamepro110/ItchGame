using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Dagger : PickupBase
{
    [SerializeField] private float m_damage = 0;
    private int m_useCount = 0;
    [SerializeField] private int m_durabilityCount = 0;
    [SerializeField] private int m_throwForce = 0;
    [SerializeField] private Vector2 m_hitBoxCenter = Vector2.zero;
    [SerializeField] private Vector2 m_hitBoxSize = Vector2.zero;
    [SerializeField] private LayerMask m_hitMask = default;
    private IHitable m_owner = null;

    private List<RaycastHit2D> hits = new List<RaycastHit2D>();

    private void Start()
    {
        Init();
        //useItemAction = UsingItem;
        pickupItem = OnPickup;
        CustomThrowAction = OnThrow;
        // TODO add collision dmg dealing
    }

    private void OnPickup()
    {
        // TODO dagger reset on pickup
        m_owner = m_movement.GetComponent<IHitable>();
    }

    private void UsingItem(GameObject obj)
    {
        m_useCount++;

        hits = new List<RaycastHit2D>(Physics2D.BoxCastAll((Vector2)transform.position + m_hitBoxCenter, m_hitBoxSize, 0, Vector2.zero, 0, m_hitMask));
        hits.ForEach(DealDmg);

        if (m_useCount > m_durabilityCount)
        {
            System.Random _rand = new System.Random(System.DateTime.Now.Millisecond);
            int num = _rand.Next(0, int.MaxValue);
            if (num % 2 == 0)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    private void DealDmg(RaycastHit2D _hit)
    {
        IHitable item = _hit.transform.GetComponent<IHitable>();
        if (item != null)
        {
            if (item != m_owner)
            {
                item.Hit(m_damage, m_movement.gameObject, gameObject); // TODO test hit check
            }
        }
    }

    private void OnThrow()
    {
        m_owner = null;

        int dir = (int)(m_movement.PlayerDir == PlayerDirection.left ? Direction.Left : Direction.Right);
        Vector2 v2 = new Vector2(dir, 3);

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

        m_RB.AddForce(v2 * m_throwForce, ForceMode2D.Impulse);
        transform.localRotation = Quaternion.identity;
        transform.localRotation = meh;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position + (Vector3)m_hitBoxCenter, m_hitBoxSize);
    }
}