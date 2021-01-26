using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawnBehaviour : MonoBehaviourPunCallbacks
{
    private Dictionary<int, GameObject> m_PlayerDict = new Dictionary<int, GameObject>();

    private PhotonView m_photonView = null;

    internal IReadOnlyDictionary<int, GameObject> GetPlayerDict => m_PlayerDict;

    private void Start()
    {
        // kill rigidbody on players that are not the local
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }

        m_photonView = GetComponent<PhotonView>();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        m_PlayerDict.Add(m_photonView.ViewID, gameObject);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        m_PlayerDict.Remove(m_photonView.ViewID);
    }
}