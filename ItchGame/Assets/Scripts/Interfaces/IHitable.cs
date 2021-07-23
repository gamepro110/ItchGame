using UnityEngine;

public interface IHitable
{
    void Hit(float dmg, GameObject owner = null, GameObject hitter = null);

    void Heal(float heal);
}