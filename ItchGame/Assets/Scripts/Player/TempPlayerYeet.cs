using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using System.Collections;

public class TempPlayerYeet : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject m_ball = null;
    private PlayerMovement m_movement = null;
    private InputManager m_input = null;
    private Vector3 m_bulletSpawnDir = Vector3.zero;
    [SerializeField, Range(0, 10)] private float m_bulletLifeTime = 0;

    private void Start()
    {
        if (photonView.IsMine)
        {
            m_input = FindObjectOfType<InputManager>();
            m_movement = GetComponent<PlayerMovement>();

            m_input.TempYeet = Yeet;
        }
    }

    private void Yeet(InputAction.CallbackContext obj)
    {
        Direction dir = m_movement.PlayerDir == PlayerDirection.left ? Direction.Left : Direction.Right;
        m_bulletSpawnDir = new Vector3((float)dir, 0);
        GameObject go = PhotonNetwork.Instantiate(m_ball.name, transform.position + m_bulletSpawnDir, Quaternion.identity);
        go.GetComponent<Bullet>().SetDirection(dir);
        StartCoroutine(BulletCleanup(go));
    }

    private IEnumerator BulletCleanup(GameObject go)
    {
        yield return new WaitForSeconds(m_bulletLifeTime);
        PhotonNetwork.Destroy(go);
    }
}