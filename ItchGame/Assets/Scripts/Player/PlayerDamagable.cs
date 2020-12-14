using UnityEngine;
using TMPro;

public class PlayerDamagable : MonoBehaviour, IHitable
{
    private float m_damage = 0;
    public float TotalDamage { get => m_damage; }

    public TMP_Text txt;
    private Rigidbody2D m_RB;

    private void Start()
    {
        txt = FindObjectOfType<TMP_Text>();
        m_RB = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        txt.text = string.Format("{0:0.00; 0.00;ZERO}%", m_damage);
    }

    public void Hit(float dmg, bool heal = false, GameObject hitter = null)
    {
        m_damage += dmg;

        if (!heal)
        {
            //start dmg flashing...
        }

        if (m_damage < 0)
        {
            m_damage = 0;
        }

        if (hitter != null)
        {
            Vector2 newVel = Vector2.zero;
            newVel += new Vector2((int)hitter.GetComponent<Bullet>().m_currentDir, 0);
            newVel *= hitter.GetComponent<Collider2D>().bounciness;
            newVel.y = 0;
            m_RB.velocity += newVel;
            Debug.Log(m_RB.velocity + " " + m_RB.gameObject);
        }
    }
}