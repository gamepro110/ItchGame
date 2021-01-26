using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerDamagable : MonoBehaviourPunCallbacks, IHitable
{
    [SerializeField] private float m_damage = 0;
    [SerializeField] private float m_dmgCap = 0;
    [SerializeField] private string m_deathText = string.Empty;

    public float TotalDamage { get => m_damage; }

    public TMP_Text txt;
    private Rigidbody2D m_RB;

    [SerializeField] private SpriteOpacityManager m_objSprite;

    private void Start()
    {
        if (photonView.IsMine)
        {
            txt = FindObjectOfType<TMP_Text>();
            m_RB = GetComponent<Rigidbody2D>();

            m_objSprite = GetComponent<SpriteOpacityManager>();
        }
    }

    private void LateUpdate()
    {
        if (photonView.IsMine)
        {
            txt.text = string.Format("{0:0.00; 0.00;ZERO;}%", m_damage);
            if (m_damage > m_dmgCap)
            {
                txt.text = m_deathText;
                gameObject.SetActive(false);
            }
        }
    }

    // TODO make RPC
    public void Hit(float dmg, GameObject owner = null, GameObject hitter = null)
    {
        if (gameObject != owner)
        {
            m_damage += dmg;

            //start dmg flashing...

            if (m_damage > m_dmgCap)
            {
                m_damage = 0;
                gameObject.SetActive(false);
            }

            if (hitter != null)
            {
                //    Vector2 newVel = Vector2.zero;
                //    newVel += new Vector2((int)hitter.GetComponent<BulletBase>().GetDir, 0);
                //    //newVel *= hitter.GetComponent<Collider2D>().bounciness;
                //    newVel.y = 0;
                //    m_RB.velocity += newVel;
                //    Debug.Log(m_RB.velocity + " " + m_RB.gameObject);
            }

            HPToVisibility();
        }
    }

    public void Heal(float amount)
    {
        m_damage -= amount;

        if (m_damage < 1) m_damage = 0;

        HPToVisibility();
    }

    private void HPToVisibility()
    {
        float op = (1 - (m_damage / m_dmgCap));
        Debug.Log(op);

        m_objSprite.UpdateOp(op);
    }
}