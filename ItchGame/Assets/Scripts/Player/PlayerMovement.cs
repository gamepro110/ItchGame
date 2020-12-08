using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
{
    private InputManager m_input = null;
    private PlayerJump m_jump;

    [SerializeField] private float m_movementSpeed = 5f;
    [SerializeField] private PlayerState m_state = PlayerState.idle;
    [SerializeField] private PlayerDirection m_dir = PlayerDirection.right;

    internal PlayerDirection PlayerDir { get => m_dir; }
    internal PlayerState PlayerState { get => m_state; }

    private void Start()
    {
        m_input = FindObjectOfType<InputManager>();
        m_jump = GetComponent<PlayerJump>();

        if (transform.position.x < 0)
        {
            m_dir = PlayerDirection.right;
        }
        else
        {
            m_dir = PlayerDirection.left;
        }

        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            m_state = m_input.LeftStick.x < 0 || m_input.LeftStick.x > 0 ? PlayerState.runing : m_jump.GetJumpState != JumpState.grounded ? PlayerState.jumping : PlayerState.idle;

            switch (m_input.LeftStick.x)
            {
                case float a when a < 0:
                    {
                        m_dir = PlayerDirection.left;
                        break;
                    }
                case float a when a > 0:
                    {
                        m_dir = PlayerDirection.right;
                        break;
                    }
                default:
                    break;
            }

            transform.position += (Vector3)m_input.LeftStick * m_movementSpeed * Time.deltaTime;
        }
    }
}

internal enum PlayerDirection
{
    right = 0,
    left,
}

internal enum PlayerState
{
    idle = 0,
    runing,
    jumping,
}