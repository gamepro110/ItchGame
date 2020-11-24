﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
        }
        else
        {
            Instantiate(Resources.Load<GameObject>("Player"), Vector3.zero, Quaternion.identity);
            Debug.LogWarning("You are offline");
        }

        Destroy(gameObject);
    }
}