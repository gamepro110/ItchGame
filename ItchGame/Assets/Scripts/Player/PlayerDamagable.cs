using UnityEngine;
using TMPro;

public class PlayerDamagable : MonoBehaviour, IHitable
{
    float m_damage = 0;
    public float TotalDamage { get => m_damage; }

    public TMP_Text txt;

    private void Start()
    {
        txt = FindObjectOfType<TMP_Text>();
    }

    void LateUpdate()
    {
        txt.text = string.Format("{0}%", m_damage);
    }

    public void Hit(float dmg)
    {
        m_damage += dmg;
    }
}