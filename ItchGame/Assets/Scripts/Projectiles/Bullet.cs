using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public enum Direction
{
    Left = -1,
    Right = 1,
}

public class Bullet : MonoBehaviourPunCallbacks
{
    [SerializeField] private float m_bulletSpeed;
    private IHitable m_hitable = null;
    [SerializeField] private float m_dmg = 2;
    [SerializeField] private Direction m_currentDir = Direction.Right;
    [SerializeField] private LayerMask m_layers = default;
    private RaycastHit2D m_hit = default;

    internal void SetDirection(Direction _dir)
    {
        m_currentDir = _dir;
    }

    private void Update()
    {
        transform.position += (new Vector3((int)m_currentDir, 0, 0) * m_bulletSpeed) * Time.deltaTime;

        m_hit = Physics2D.CircleCast(transform.position, 1, Vector2.zero, 0, m_layers);
        m_hitable = m_hit.transform?.GetComponent<IHitable>();

        if (m_hitable != null)
        {
            m_hitable.Hit(m_dmg);
            PhotonNetwork.Destroy(gameObject);
        }
    }
}