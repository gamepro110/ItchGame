using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using System.Collections;

public class BazookaBullet : BulletBase
{
    private Vector3 m_direction = Vector3.zero;
    private List<Collider2D> colls;

    [SerializeField] private float m_explosionDMG;

    [SerializeField, Range(0.5f, 10)] private float ExplosionDmgRange;

    [SerializeField] private GameObject explosioneffect;

    private void Start()
    {
        m_direction = new Vector3((int)m_dir, 0);

        StartCoroutine(SelfDestroy());
    }

    private void Update()
    {
        transform.position += m_direction * m_speed * Time.deltaTime;

        if (HitCheck())
        {
            m_hitable?.Hit(m_damage, m_owner, gameObject);

            BazookaBoom();
        }
        if (m_hitable != null)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }

    void BazookaBoom()
    {
        PhotonNetwork.Instantiate(explosioneffect.name, transform.position, Quaternion.identity);

        colls = new List<Collider2D>(Physics2D.OverlapCircleAll(transform.position, ExplosionDmgRange, m_hitLayerMask));


        foreach (var item in colls)
        {
            item.GetComponent<IHitable>()?.Hit(m_explosionDMG, m_owner, gameObject);
        }
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(3f);
        PhotonNetwork.Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, ExplosionDmgRange);
    }
}