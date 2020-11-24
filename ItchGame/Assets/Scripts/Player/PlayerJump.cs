using UnityEngine;
using Photon.Pun;

internal enum JumpState
{
    grounded = 0,
    jumped,
    doubleJumped,
}

public class PlayerJump : MonoBehaviourPunCallbacks
{
    private InputManager m_input = null;

    private RaycastHit2D m_hit = default;

    [SerializeField] private JumpState m_jumpState = JumpState.grounded;
    [SerializeField] private LayerMask m_layers = default;
    [SerializeField] private float m_jumpForce = 5f;
    [SerializeField, Range(0.1f, 0.99f)] private float m_jumpForceMultiplier = 1f;
    [SerializeField] private bool m_isGrounded = false;
    [SerializeField] private float m_groundedRayLength = 1.0f;

    internal JumpState GetJumpState { get => m_jumpState; }

    private void Start()
    {
        m_input = FindObjectOfType<InputManager>();
        if (photonView.IsMine)
        {
            m_input.jump = PlayerJumpAction;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            m_isGrounded = GroundCheck;

            if (GroundCheck)
            {
                m_jumpState = JumpState.grounded;
            }
        }
    }

    private void PlayerJumpAction(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 vel = Vector2.zero;

        switch (m_jumpState)
        {
            case JumpState.grounded:
                {
                    vel = Vector2.up * m_jumpForce;
                    break;
                }
            case JumpState.jumped:
                {
                    vel = Vector2.up * m_jumpForce * (m_jumpForceMultiplier);
                    break;
                }
        }

        //TODO calculate jump
        Debug.Log("CALCULATE JUMP", this);

        switch (m_jumpState)
        {
            case JumpState.grounded:
                {
                    m_jumpState = JumpState.jumped;
                }
                break;

            case JumpState.jumped:
                {
                    m_jumpState = JumpState.doubleJumped;
                }
                break;

            case JumpState.doubleJumped:
                break;
        }
    }

    private bool GroundCheck
    {
        get
        {
            m_hit = Physics2D.Raycast(transform.position, Vector2.down, m_groundedRayLength, m_layers);
            return m_hit.transform != null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * m_groundedRayLength);
    }
}