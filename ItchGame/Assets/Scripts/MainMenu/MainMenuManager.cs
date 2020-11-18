using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button StartB;
    [SerializeField] private Button OptionsB;
    [SerializeField] private Button QuitB;

    [SerializeField] private Button StartMPGame;

    void Start()
    {
        StartB.onClick.AddListener(StartTheGame);
        OptionsB.onClick.AddListener(GoToOptions);
        QuitB.onClick.AddListener(QuitTheGame);

        StartMPGame.onClick.AddListener(GoToMultiplayer);
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void StartTheGame()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinOrCreateRoom("Testing", new Photon.Realtime.RoomOptions() { MaxPlayers = 3 }, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        StartB.gameObject.SetActive(false);
        StartMPGame.gameObject.SetActive(true);
    }

    void GoToMultiplayer()
    {
        PhotonNetwork.LoadLevel(1);
    }

    void GoToOptions()
    {

    }

    void QuitTheGame()
    {
        Application.Quit();
    }
}