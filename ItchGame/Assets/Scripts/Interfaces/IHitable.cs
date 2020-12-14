using UnityEngine;

internal interface IHitable
{
    void Hit(float dmg, bool heal = false, GameObject hitter = null);
}