using UnityEngine;
using Photon.Pun;

public class RifleBullet : BulletBase
{
    private Vector3 m_direction = Vector3.zero;

    private void Start()
    {
        m_direction = new Vector3((int)m_dir, 0);
    }

    private void Update()
    {
        transform.position += m_direction * m_speed * Time.deltaTime;

        if (HitCheck())
        {
            m_hitable?.Hit(m_damage, m_owner, gameObject);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}