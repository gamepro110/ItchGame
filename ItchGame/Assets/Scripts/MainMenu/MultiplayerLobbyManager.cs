﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using TMPro;

public class MultiplayerLobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button MainMenuB;
    [SerializeField] private Button ConnectB;
    [SerializeField] private Button StartMultiB;
    [SerializeField] private TMP_InputField tijdelijkRoom;

    [SerializeField, Range(0, 10)] private int m_gameSceneIndex = 0;

    private void Start()
    {
        MainMenuB.onClick.AddListener(GoToMainMenu);
        ConnectB.onClick.AddListener(GoConnect);
        StartMultiB.onClick.AddListener(StartMulti);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void GoToMainMenu()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void GoConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    private void StartMulti()
    {
        if (Random.Range(0, 100) > 50)
        {
            m_gameSceneIndex = 3;
        }
        else
        {
            m_gameSceneIndex = 4;
        }
        PhotonNetwork.LoadLevel(m_gameSceneIndex);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinOrCreateRoom(tijdelijkRoom.text != string.Empty ? tijdelijkRoom.text : "Empty", new Photon.Realtime.RoomOptions() { MaxPlayers = 4 }, null);
        ConnectB.interactable = false;
    }

    //temp
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (PhotonNetwork.IsMasterClient)
        {
            StartMultiB.interactable = true;
        }
    }

    //temp
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                StartMultiB.interactable = true;
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);

        if (PhotonNetwork.CurrentRoom.PlayerCount < 2)
        {
            StartMultiB.interactable = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
        PhotonNetwork.AutomaticallySyncScene = false;
        SceneManager.LoadScene(0);
    }
}