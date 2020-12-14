using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private string m_playerPrefabStr = string.Empty;

    private void Awake()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate(m_playerPrefabStr, Vector3.zero, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load<GameObject>(m_playerPrefabStr), Vector3.zero, Quaternion.identity);
            Debug.LogWarning("You are offline");
        }

        Destroy(gameObject);
    }
}