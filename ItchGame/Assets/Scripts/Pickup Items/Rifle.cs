using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Rifle : PickupBase
{
    [SerializeField] private GameObject m_bullet = null;
    [SerializeField] private Transform m_nuzzlePos = null;
    [SerializeField, Range(1, 100)] private float m_damage = 0;
    [SerializeField, Range(1, 20)] private float m_bulletSpeed = 0;
    [SerializeField, Range(1, 10)] private int m_bulletLifetime = 0;

    [SerializeField] private int m_ammo = 0;
    [SerializeField, Range(1, 15)] private int m_minAmmo = 0;
    [SerializeField, Range(1, 30)] private int m_maxAmmo = 0;
    [SerializeField, Range(0, 5f)] private float m_fireRate = 0;
    private float m_setFireRate = 0;

    private void Start()
    {
        m_ammo = Random.Range(m_minAmmo, m_maxAmmo);

        Init();
        useItemAction = UsingItem;
        pickupItem = RiflePickup;
        CustomThrowAction = OnThrowItem;
        m_setFireRate = m_fireRate;
    }

    private void RiflePickup()
    {
        m_fireRate = 0;
    }

    private void UsingItem(GameObject obj)
    {
        if (m_ammo > 0)
        {
            if (m_fireRate == 0)
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
                m_fireRate = m_setFireRate;
            }
        }
        else
        {
            ThrowItem(Vector2.up * 10);
        }

        m_fireRate -= Time.deltaTime;
        m_fireRate = Mathf.Clamp(m_fireRate, 0, 10);
    }

    private void OnThrowItem()
    {
        m_RB.AddForce(Vector2.up * 10);
    }

    private IEnumerator BulletCleanup(GameObject go)
    {
        yield return new WaitForSeconds(m_bulletLifetime);

        if (go != null)
        {
            PhotonNetwork.Destroy(go);
        }
    }
}