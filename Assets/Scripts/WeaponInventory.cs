using UnityEngine;
using System.Collections.Generic;

public class WeaponInventory : MonoBehaviour
{
    public static WeaponInventory instance;
    public List<WeaponController> weaponList;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    public bool IsInventoryWeapon(WeaponController weapon)
    {
        return weaponList.Contains(weapon);
    }
}
