using UnityEngine;
using TMPro;

public class PlayerDamagable : MonoBehaviour, IHitable
{
    private float m_damage = 0;
    public float TotalDamage { get => m_damage; }

    public TMP_Text txt;

    private void Start()
    {
        txt = FindObjectOfType<TMP_Text>();
    }

    private void LateUpdate()
    {
        txt.text = string.Format("{0:0.00; 0.00;ZERO}%", m_damage);
    }

    public void Hit(float dmg, bool heal = false)
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
    }
}