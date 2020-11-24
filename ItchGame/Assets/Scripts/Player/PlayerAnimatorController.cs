using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator m_anim = null;
    private PlayerMovement m_movement = null;

    private void Start()
    {
        m_anim = GetComponentInChildren<Animator>();
        m_movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateAnimatorState();
    }

    private void UpdateAnimatorState()
    {
        m_anim.SetInteger("state", (int)m_movement.PlayerState);
        m_anim.SetInteger("dir", (int)m_movement.PlayerDir);
    }
}