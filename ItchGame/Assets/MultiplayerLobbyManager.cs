using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class MultiplayerLobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button MainMenuB;
    [SerializeField] private Button ConnectB;
    [SerializeField] private Button StartMultiB;

    private void Start()
    {
        MainMenuB.onClick.AddListener(GoToMainMenu);
        ConnectB.onClick.AddListener(GoConnect);
        StartMultiB.onClick.AddListener(StartMulti);

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void GoToMainMenu()
    {
        PhotonNetwork.Disconnect();
    }

    void GoConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    void StartMulti()
    {
        PhotonNetwork.LoadLevel(2);
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        PhotonNetwork.JoinOrCreateRoom("Testing", new Photon.Realtime.RoomOptions() { MaxPlayers = 3 }, null);
        ConnectB.interactable = false;
    }
    //temp
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        StartMultiB.interactable = true;
    }
    //temp
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        if (PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            StartMultiB.interactable = true;
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
        SceneManager.LoadScene(0);
    }
}