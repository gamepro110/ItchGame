using UnityEngine;
using Photon.Pun;

internal enum JumpState
{
    grounded = 0,
    jumped,
    doubleJumped,
    falling,
}

public class PlayerJump : MonoBehaviourPunCallbacks
{
    private InputManager m_input = null;
    private Rigidbody2D m_RB = null;

    private RaycastHit2D m_hit = default;

    [SerializeField] private JumpState m_jumpState = JumpState.grounded;
    [SerializeField] private LayerMask m_layers = default;
    [SerializeField] private float m_jumpForce = 5f;
    [SerializeField, Range(0.1f, 0.99f)] private float m_jumpForceMultiplier = 1f;
    [SerializeField] private bool m_isGrounded = false;
    [SerializeField] private float m_groundedRayLength = 1.0f;

    private bool hasDJumped;

    internal JumpState GetJumpState { get => m_jumpState; }

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_input = FindObjectOfType<InputManager>();
        if (photonView.IsMine)
        {
            m_input.jump_started = PlayerJumpStarted;
            m_input.jump_canceled = PlayerJumpCanceled;
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
                hasDJumped = false;
            }
            else
            {
                m_jumpState = JumpState.falling;
            }
        }
    }

    private void PlayerJumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Vector2 vel = Vector2.zero;

        switch (m_jumpState)
        {
            case JumpState.grounded:
                {
                    m_jumpState = JumpState.jumped;
                    vel = Vector2.up * m_jumpForce;
                    break;
                }

            case JumpState.jumped:
                {
                    m_RB.velocity = new Vector2(m_RB.velocity.x, m_RB.velocity.y < 0 ? 0 : m_RB.velocity.y);
                    vel = Vector2.up * m_jumpForce * (m_jumpForceMultiplier);
                    m_jumpState = JumpState.jumped;
                    break;
                }

            case JumpState.doubleJumped:
                {
                    break;
                }

            case JumpState.falling:
                {
                    if (!hasDJumped)
                    {
                        m_RB.velocity = new Vector2(m_RB.velocity.x, m_RB.velocity.y < 0 ? 0 : m_RB.velocity.y);
                        vel = Vector2.up * m_jumpForce * (m_jumpForceMultiplier);
                        m_jumpState = JumpState.doubleJumped;
                        hasDJumped = true;
                    }
                }
                break;
        }

        m_RB.velocity += vel;
    }

    private void PlayerJumpCanceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
    }

    private bool GroundCheck
    {
        get
        {
            m_hit = Physics2D.Raycast(transform.position, Vector2.down, m_groundedRayLength, m_layers);
            //m_hit = Physics2D.BoxCast(transform.position, new Vector2(m_groundedRayLength, m_groundedRayLength + 0.1f), 0, Vector2.down);
            //if (m_hit.transform.GetComponent<PlayerJump>() != null)
            //{
            //    return false;
            //}
            //else
            //{
            return m_hit.transform != null;
            //}
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * m_groundedRayLength);
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y - 0.3f, transform.position.z), new Vector2(m_groundedRayLength, m_groundedRayLength));
    }
}