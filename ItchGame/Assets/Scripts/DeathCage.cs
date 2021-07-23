using UnityEngine;
using Photon.Pun;

public class DeathCage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        IHitable m_hitable = null;
        m_hitable = collision.gameObject.GetComponent<IHitable>();
        if (m_hitable != null)
        {
            collision.gameObject.GetComponent<PlayerDamagable>()?.Hit(200);
            collision.gameObject.SetActive(false);
            return;
        }

        IPickupAble m_pickupAble = null;
        m_pickupAble = collision.gameObject.GetComponent<IPickupAble>();
        if (m_pickupAble != null)
        {
            try
            {
                PickupBase pickup = collision.gameObject.GetComponent<PickupBase>();
                pickup.NetworkDestroy();
            }
            catch (System.Exception)
            {
                Debug.LogError("IPickupable hit deathcage but could not be destroyed by photonNetwork.Destroy(GameObject go). destroyed locally only");
                Destroy(collision.gameObject);
            }

            return;
        }

        collision.gameObject.SetActive(false);
    }
}