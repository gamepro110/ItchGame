using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class BazookaBullet : BulletBase
{
    private Vector3 m_direction = Vector3.zero;


    [SerializeField, Range(0.5f, 10)] private float ExplosionDmgRange;

    private RaycastHit2D ExplosionRangeRayHit;
    [SerializeField]private LayerMask layer;

    private void Start()
    {
        m_direction = new Vector3((int)m_dir, 0);

    }

    private void Update()
    {
        transform.position += m_direction * m_speed * Time.deltaTime;

        if (HitCheck())
        {
            //m_hitable?.Hit(m_damage, m_owner, gameObject);

            BazookaBoom();
        }
    }

    void BazookaBoom()
    {
        ExplosionRangeRayHit = Physics2D.CircleCast(transform.position, ExplosionDmgRange, Vector2.zero, 0, layer);

        List<IHitable> hits = new List<IHitable>(ExplosionRangeRayHit.collider?.GetComponents<IHitable>());
        hits.ForEach(x => Debug.Log(x.ToString()));
        //hits.ForEach(x => x.Hit(m_damage, m_owner, gameObject));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, ExplosionDmgRange);
    }
}