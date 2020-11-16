using UnityEngine;
using TMPro;

public class PlayerJump : MonoBehaviour
{
    private enum JumpState
    {
        grounded = 0,
        jumped,
        doubleJumped,
    }

    private InputManager m_input = null;
    private Rigidbody2D m_RB = null;

    private RaycastHit2D m_hit = default;

    [SerializeField] private JumpState m_jumpState = JumpState.grounded;
    [SerializeField] private LayerMask m_layers = default;
    [SerializeField] private float m_jumpForce = 5f;
    [SerializeField, Range(0.1f, 0.99f)] private float m_jumpForceMultiplier = 1f;
    [SerializeField] private bool m_isGrounded = false;
    [SerializeField] private float m_groundedRayLength = 1.0f;

#if UNITY_EDITOR
    public TMP_Text txt;
#endif

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_input = FindObjectOfType<InputManager>();
        m_input.jump = PlayerJumpAction;
    }

    private void Update()
    {
        m_isGrounded = GroundCheck;

        if (m_isGrounded)
        {
            m_jumpState = JumpState.grounded;
        }
        else if (!m_isGrounded && m_jumpState == JumpState.jumped)
        {
            m_jumpState = JumpState.doubleJumped;
        }
    }

#if UNITY_EDITOR

    private void LateUpdate()
    {
        txt.text = string.Format("isGrounded: {0}\nJumpState: {1}", m_isGrounded, m_jumpState);
    }

#endif

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
        m_RB.velocity += vel;
        m_jumpState++;
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