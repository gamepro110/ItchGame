using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawnBehaviour : MonoBehaviourPunCallbacks
{
    [SerializeField] private Dictionary<int, GameObject> m_PlayerDict = new Dictionary<int, GameObject>();

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

        photonView.RPC("RegisterToHost", RpcTarget.All, photonView.ViewID);

        //m_PlayerDict.Add(photonView.ViewID, gameObject);
        //List<GameObject> items = new List<GameObject>(m_PlayerDict.Values);
        //items.ForEach(x => Debug.Log(">>>>> START " + x.name));
    }

    [PunRPC]
    public void RegisterToHost(int id, PhotonMessageInfo info)
    {
        //TODO test correctness of this...
        GameObject go = new List<PhotonView>(FindObjectsOfType<PhotonView>()).Find(x => x.ViewID == id).gameObject;
        m_PlayerDict.Add(id, go);
    }
}