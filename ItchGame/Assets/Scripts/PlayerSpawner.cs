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
            int i = 0;
            foreach (var item in PhotonNetwork.CurrentRoom.Players)
            {
                i = item.Key;
                break;
            }

            GameObject go = PhotonNetwork.Instantiate(m_playerPrefabStr, new Vector3(0, i, 0), Quaternion.identity);
            go.name += PhotonNetwork.NickName;
        }
        else
        {
            Instantiate(Resources.Load<GameObject>(m_playerPrefabStr), Vector3.zero, Quaternion.identity);
            Debug.LogWarning("You are offline");
        }

        Destroy(gameObject);
    }
}