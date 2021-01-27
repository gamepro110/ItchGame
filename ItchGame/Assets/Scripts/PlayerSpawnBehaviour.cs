using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerSpawnBehaviour : MonoBehaviourPunCallbacks
{
    [SerializeField] private Dictionary<int, GameObject> m_PlayerDict = new Dictionary<int, GameObject>();
    [SerializeField] private List<GameObject> test = new List<GameObject>();

    internal IReadOnlyDictionary<int, GameObject> GetPlayerDict => m_PlayerDict;

    private void Start()
    {
        // kill rigidbody on players that are not the local
        if (!photonView.IsMine)
        {
            Destroy(GetComponent<Rigidbody2D>());
        }

        photonView.RPC("RegisterPlayer", RpcTarget.All);

        //m_PlayerDict.Add(photonView.ViewID, gameObject);
        //List<GameObject> items = new List<GameObject>(m_PlayerDict.Values);
        //items.ForEach(x => Debug.Log(">>>>> START " + x.name));
    }

    private void LateUpdate()
    {
        test = new List<GameObject>(m_PlayerDict.Values);
    }

    [PunRPC]
    public void RegisterPlayer(PhotonMessageInfo info)
    {
        if (!m_PlayerDict.ContainsKey(info.photonView.ViewID))
        {
            //TODO test correctness of this...
            GameObject go = new List<PhotonView>(FindObjectsOfType<PhotonView>()).Find(x => x.ViewID == info.photonView.ViewID).gameObject;
            m_PlayerDict.Add(info.photonView.ViewID, go);
        }
        else
        {
            string thing = string.Empty;
            test.ForEach(x => thing += $"name :\t{x.name}\n");

            //Debug.Log($"recieved id: {info.photonView.ViewID}, existing: {thing}", this);
        }
    }
}