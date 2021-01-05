using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rifle : PickupBase
{
    private PlayerMovement m_movement = null;
    [SerializeField] private GameObject m_bullet = null;
    [SerializeField] private Transform m_nuzzlePos = null;
    [SerializeField, Range(1, 10)] private float m_damage = 0;
    [SerializeField, Range(1, 20)] private float m_bulletSpeed = 0;
    [SerializeField, Range(1, 10)] private int m_bulletLifetime = 0;

    private int m_ammo = 0;
    [SerializeField, Range(10, 15)] private int m_minAmmo = 0;
    [SerializeField, Range(15, 30)] private int m_maxAmmo = 0;

    private void Start()
    {
        m_ammo = Random.Range(m_minAmmo, m_maxAmmo);
        Debug.Log(m_ammo);
        Init();
        useItemAction = UsingItem;
        pickupItem = RiflePickup;
    }

    private void RiflePickup()
    {
        m_movement = transform.parent.GetComponentInParent<PlayerMovement>();
        GetComponent<GunDir>().SetMovement(m_movement);
    }

    private void UsingItem(GameObject obj)
    {
        if (m_ammo > 1)
        {
            Direction dir = m_movement.PlayerDir == PlayerDirection.left ? Direction.Left : Direction.Right;
            GameObject go = PhotonNetwork.Instantiate(m_bullet.name, m_nuzzlePos.position, Quaternion.identity);
            BulletBase bullet = go.GetComponent<BulletBase>();
            bullet.SetDirection(dir);
            bullet.SetDamage(m_damage);
            bullet.SetSpeed(m_bulletSpeed);
            bullet.SetOwner(m_movement.gameObject);
            StartCoroutine(BulletCleanup(go));
            m_ammo--;
        }
        else
        {
            Debug.Log("yeet gun");
            m_pickup.m_heldItem = null;
            transform.SetParent(null);
            GetComponent<GunDir>().SetMovement(null);
        }
    }

    private IEnumerator BulletCleanup(GameObject go)
    {
        yield return new WaitForSeconds(m_bulletLifetime);
        PhotonNetwork.Destroy(go);
    }
}