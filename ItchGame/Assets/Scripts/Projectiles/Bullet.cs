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

    internal void SetDirection(Direction _dir)
    {
        m_currentDir = _dir;
    }

    void Update()
    {
        transform.position += (new Vector3((int)m_currentDir, 0, 0) * m_bulletSpeed) * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_hitable = collision.gameObject.GetComponent<IHitable>();
        if (m_hitable != null)
        {
            m_hitable.Hit(m_dmg);
        }
        Destroy(gameObject);
    }
}