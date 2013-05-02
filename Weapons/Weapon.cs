using UnityEngine;

/**
 * Базовый класс оружия.
 */
class Weapon : MonoBehaviour
{
    public int weaponType = 10;

    public Transform target;

    public void setTarget(Transform target)
    {
        this.target = target;
    }

    public Transform getTarget()
    {
        return target;
    }

    public bool hasTarget()
    {
        return target != null;
    }
}