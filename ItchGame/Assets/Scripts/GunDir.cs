using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDir : MonoBehaviour
{
    private PlayerMovement m_movement = null;
    private Vector3 m_scale = Vector3.zero;

    private void LateUpdate()
    {
        if (m_movement != null)
        {
            m_scale = new Vector3((int)m_movement?.PlayerDir * -1, 1);
            transform.localScale = m_scale;
        }
    }

    internal void SetMovement(PlayerMovement mov)
    {
        m_movement = mov;
    }
}