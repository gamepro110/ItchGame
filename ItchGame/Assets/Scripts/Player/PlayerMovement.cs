using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private InputManager m_input = null;
    private Rigidbody2D m_RB = null;

    [SerializeField] private float m_movementSpeed = 5f;

    private void Start()
    {
        m_RB = GetComponent<Rigidbody2D>();
        m_input = FindObjectOfType<InputManager>();
    }

    private void Update()
    {
        m_RB.transform.position += (Vector3)m_input.LeftStick * m_movementSpeed * Time.deltaTime;
    }
}