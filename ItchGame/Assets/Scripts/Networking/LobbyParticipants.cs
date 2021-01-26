using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyParticipants : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text m_text = null;
    private string m_msg = string.Empty;

    private void Start()
    {
        m_text.text = string.Empty;
    }

    private void LateUpdate()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            m_text.text = GetParticipants();
        }
    }

    // TODO fix bug
    private string GetParticipants()
    {
        m_msg = $"{(PhotonNetwork.IsMasterClient ? "\tyou are the [HOST]" : string.Empty)}";
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            m_msg += string.Format("\t{0}\n", item.Value.NickName != string.Empty ? item.Value.NickName : "GUEST_" + System.Environment.UserName[0] + System.Environment.UserName[2]);
        }
        return m_msg;
    }
}