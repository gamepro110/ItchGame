using UnityEngine;
using Photon.Pun;

public class BazookaBullet : BulletBase
{
    private Vector3 m_direction = Vector3.zero;


    [SerializeField, Range(0.5f, 10)] private float ExplosionDmgRange;

    private RaycastHit2D ExplosionRangeRayHit;

    private void Start()
    {
        m_direction = new Vector3((int)m_dir, 0);

        ExplosionRangeRayHit = Physics2D.CircleCast(transform.position, ExplosionDmgRange, new Vector2());
    }

    private void Update()
    {
        transform.position += m_direction * m_speed * Time.deltaTime;

        if (HitCheck())
        {
            m_hitable?.Hit(m_damage, m_owner, gameObject);

            BazookaBoom();
        }
    }

    void BazookaBoom()
    {
        if (ExplosionRangeRayHit.collider.GetComponent<IHitable>() != null)
        {
            ExplosionRangeRayHit.collider.GetComponent<IHitable>().Hit(m_damage, m_owner, gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, ExplosionDmgRange);
    }
}