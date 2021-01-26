using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpriteOpacityManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private SpriteRenderer[] sprites;

    public void UpdateOp(float op)
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                Color tmp = sprites[i].color;
                tmp.a = op;
                sprites[i].color = tmp;
            }
        }
    }
}