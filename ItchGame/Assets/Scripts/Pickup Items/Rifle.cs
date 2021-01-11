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

    [SerializeField] private int m_ammo = 0;
    [SerializeField, Range(10, 15)] private int m_minAmmo = 0;
    [SerializeField, Range(15, 30)] private int m_maxAmmo = 0;
    [SerializeField, Range(0.01f, 5f)] private float m_fireRate = 0;
    private float m_setFireRate = 0;

    private void Start()
    {
        m_ammo = Random.Range(m_minAmmo, m_maxAmmo);
        Debug.Log(m_ammo);
        Init();
        useItemAction = UsingItem;
        pickupItem = RiflePickup;
        m_setFireRate = m_fireRate;
        CustomThrowAction = OnItemThrow;
    }

    private void RiflePickup()
    {
        m_fireRate = 0;
        m_movement = transform.parent.GetComponentInParent<PlayerMovement>();
        GetComponent<GunDir>().SetMovement(m_movement);
    }

    private void UsingItem(GameObject obj)
    {
        if (m_fireRate < 0)
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
                m_fireRate = m_setFireRate;
            }
            else
            {
                ThrowItem(Vector2.up * 6);
            }
        }
        else
        {
            Mathf.Clamp(m_fireRate -= Time.deltaTime, 0, 100);
        }
    }

    private void OnItemThrow()
    {
        GetComponent<GunDir>().SetMovement(null);
        Debug.Log("yeet gun");
    }

    private IEnumerator BulletCleanup(GameObject go)
    {
        yield return new WaitForSeconds(m_bulletLifetime);
        PhotonNetwork.Destroy(go);
    }
}