using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ParticleSystemDestroy : MonoBehaviourPunCallbacks
{
    private float destoryTime = 1;

    public override void OnEnable()
    {
        base.OnEnable();

        destoryTime = GetComponent<ParticleSystem>().main.duration;

        StartCoroutine(DestroySelf());
    }

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(destoryTime);
        PhotonNetwork.Destroy(gameObject);
    }
}