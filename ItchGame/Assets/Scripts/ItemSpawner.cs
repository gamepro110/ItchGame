using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            items.ForEach(x => PhotonNetwork.Instantiate("pickups/" + x.name, new Vector3(Random.Range(-7, 7), 6), Quaternion.identity));
        }
    }
}