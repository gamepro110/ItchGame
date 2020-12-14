using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyParticipants : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text m_text = null;
    private string m_msg = string.Empty;

    private void Start()
    {
        m_text.text = m_msg;
    }

    private void LateUpdate()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
        {
            UpdateParticipants();
        }
    }

    private void UpdateParticipants()
    {
        m_msg = string.Empty;
        foreach (var item in PhotonNetwork.CurrentRoom.Players)
        {
            m_msg += string.Format("\t{0}{1}\n", item.Value.NickName, PhotonNetwork.IsMasterClient ? "You`re the [HOST]" : string.Empty);
        }
        m_text.text = m_msg;
    }
}