using UnityEngine;

public class TestTrap : MonoBehaviour
{
    [SerializeField] private float m_dmg = 2f;
    private IHitable m_hitable = null;
    [SerializeField] private LayerMask m_layers = default;
    private RaycastHit2D m_hit = default;

    private void Update()
    {
        m_hit = Physics2D.CircleCast(transform.position, 1, Vector2.zero, 0, m_layers);
        m_hitable = m_hit.transform?.GetComponent<IHitable>();
        m_hitable?.Hit(m_dmg);
    }
}