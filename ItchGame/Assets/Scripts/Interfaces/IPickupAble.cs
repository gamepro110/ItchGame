using UnityEngine;

public interface IPickupAble
{
    /// <summary>
    /// called when u pickup an IPickupAble item
    /// </summary>
    /// <param name="parent">parent transform</param>
    void PickupItem(Transform parent);

    /// <summary>
    /// what the pickup has to do when the player uses it.
    /// includes decreasing use count and yetting of object
    /// </summary>
    /// <param name="obj"></param>
    void UseItem(GameObject obj);

    /// <summary>
    /// item gets thrown after condition is met
    /// </summary>
    void ThrowItem(Vector2 dir, bool enableCollider = false);
}