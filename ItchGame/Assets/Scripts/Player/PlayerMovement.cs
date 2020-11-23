using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPunCallbacks
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
        if (photonView.IsMine)
        {
            m_RB.transform.position += (Vector3)m_input.LeftStick * m_movementSpeed * Time.deltaTime;
        }
    }
}