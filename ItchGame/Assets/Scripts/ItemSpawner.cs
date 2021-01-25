﻿using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> items;

    private void Start()
    {
        foreach (var item in items)
        {
            PhotonNetwork.Instantiate(item.name, new Vector3(Random.Range(-8, 8), 6), Quaternion.identity);
        }
    }
}