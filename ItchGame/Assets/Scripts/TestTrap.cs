using UnityEngine;

public class TestTrap : MonoBehaviour
{
    [SerializeField] private float m_dmg = 2f;
    [SerializeField, Range(1f,7f)] private float m_forceMultiplier = 2f;
    private IHitable m_hit = null;

    void OnTriggerEnter2D(Collider2D col)
    {
        m_hit = col.gameObject.GetComponent<IHitable>();
        if (m_hit != null)
        {
            m_hit.Hit(m_dmg);
            Rigidbody2D m_rb = col.gameObject.GetComponent<Rigidbody2D>();
            m_rb.velocity = ((transform.position - col.transform.position) * -1) * m_forceMultiplier;
        }
        m_hit = null;
    }
}
