using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHitable m_hitable = null;
        m_hitable = collision.gameObject.GetComponent<IHitable>();
        if (m_hitable != null)
        {
            collision.gameObject.SetActive(false);
            return;
        }

        IPickupAble m_pickupAble = null;
        m_pickupAble = collision.gameObject.GetComponent<IPickupAble>();
        if (m_pickupAble != null)
        {
            collision.gameObject.SetActive(false);
            return;
        }
    }
}