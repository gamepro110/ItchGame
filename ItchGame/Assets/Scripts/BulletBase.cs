using UnityEngine;
using Photon.Pun;

public enum Direction
{
    Left = -1,
    Right = 1,
}

public class BulletBase : MonoBehaviourPunCallbacks
{
    protected IHitable m_hitable = null;
    private RaycastHit2D m_rayhit = default;
    protected Direction m_dir = Direction.Left;
    protected float m_damage = 0;
    protected float m_speed = 0;
    protected GameObject m_owner = null;
    [SerializeField] protected LayerMask m_hitLayerMask = default;

    protected bool HitCheck()
    {
        m_rayhit = Physics2D.CircleCast(transform.position, 1, Vector2.up, 0, m_hitLayerMask);

        m_hitable = m_rayhit.transform.GetComponent<IHitable>();

        if (m_hitable != null)
        {
            return true;
        }

        return false;
    }

    internal void SetDirection(Direction _dir)
    {
        m_dir = _dir;
    }

    internal void SetDamage(float _dmg)
    {
        m_damage = _dmg;
    }

    internal void SetSpeed(float _sped)
    {
        m_speed = _sped;
    }

    internal void SetOwner(GameObject go)
    {
        m_owner = go;
    }

    internal Direction GetDir => m_dir;
}