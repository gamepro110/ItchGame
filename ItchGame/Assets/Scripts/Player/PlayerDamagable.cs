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

    public void Hit(float dmg, GameObject owner = null, GameObject hitter = null)
    {
        if (gameObject != owner)
        {
            m_damage += dmg;

            //start dmg flashing...

            if (m_damage < 1)
            {
                m_damage = 0;
                gameObject.SetActive(false);
            }

            if (hitter != null)
            {
                Vector2 newVel = Vector2.zero;
                newVel += new Vector2((int)hitter.GetComponent<BulletBase>().GetDir, 0);
                newVel *= hitter.GetComponent<Collider2D>().bounciness;
                newVel.y = 0;
                m_RB.velocity += newVel;
                Debug.Log(m_RB.velocity + " " + m_RB.gameObject);
            }
        }
    }

    public void Heal(float heal)
    {
        m_damage -= heal;
    }
}