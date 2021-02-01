using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpriteOpacityManager : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private Color m_color;
    private List<SpriteRenderer> m_spriteRenderers;

    private void Start()
    {
        m_spriteRenderers = new List<SpriteRenderer>(GetComponentsInChildren<SpriteRenderer>());
        m_color = m_spriteRenderers[0].color;
    }

    public void UpdateOp(float op)
    {
        if (photonView.IsMine)
        {
            m_color.a = op;
        }
    }

    private void LateUpdate()
    {
        m_spriteRenderers.ForEach(x => x.color = m_color);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(m_color.a);
        }

        if (stream.IsReading)
        {
            m_color.a = (float)stream.ReceiveNext();
        }
    }
}