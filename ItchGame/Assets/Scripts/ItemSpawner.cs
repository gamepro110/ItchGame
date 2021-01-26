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
            foreach (var item in items)
            {
                PhotonNetwork.Instantiate("pickups/" + item.name, new Vector3(Random.Range(-8, 8), 6), Quaternion.identity);
            }
        }
    }
}