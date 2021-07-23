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

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        if (PhotonNetwork.NickName == string.Empty)
        {
            PhotonNetwork.NickName = $"GUEST_{Random.Range(0, 99)}";
        }
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
        m_msg = $"{(PhotonNetwork.IsMasterClient ? "\tyou are the [HOST]\n" : string.Empty)}";
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            m_msg += string.Format("\t{0}\n", item.Value.NickName);
        }
        return m_msg;
    }
}