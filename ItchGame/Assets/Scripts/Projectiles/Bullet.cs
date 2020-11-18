using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speedMultiplier;
    private bool m_isGrounded;
    private RaycastHit2D m_hit;
    [SerializeField]private float boxlenght;

    private void OnEnable()
    {
        if (speedMultiplier == 0)
        {
            speedMultiplier = 1;
        }
    }

    void Update()
    {
        transform.position += (transform.right * speedMultiplier) * Time.deltaTime;

        m_isGrounded = GroundCheck;
        if (m_isGrounded)
        {
            Debug.Log("Did it");
        }
    }

    private bool GroundCheck
    {
        get
        {
            m_hit = Physics2D.CircleCast(transform.position, boxlenght / 10, transform.position);
            return m_hit.transform != null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, boxlenght / 10);
    }
}