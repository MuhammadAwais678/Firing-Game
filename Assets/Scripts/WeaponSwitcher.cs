using System;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public GameObject[] weapons;

    private int currentWeaponIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ActivateWeapon(currentWeaponIndex);
    }


    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0) {
            ScrollNextWepon();
        }
        else if (scroll < 0) { 
            ScrollPreviousWeapon();
        }


        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                currentWeaponIndex = i;
                ActivateWeapon(currentWeaponIndex);
            }
        }
    }

    void ScrollNextWepon()
    {
        int startIndex = currentWeaponIndex + 1;

        for(int i=startIndex; i<weapons.Length; i++)
        {
            if (WeaponInventory.instance.IsInventoryWeapon(weapons[i].GetComponent<WeaponController>()))
            {
                ActivateWeapon(i);
                return;
            }
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            if (WeaponInventory.instance.IsInventoryWeapon(weapons[i].GetComponent<WeaponController>()))
            {
                ActivateWeapon(i);
                return;
            }
        }
    }


    void ScrollPreviousWeapon()
    {
        int previousIndex = currentWeaponIndex - 1;

        for (int i = previousIndex; i >= 0; i--)
        {
            if (WeaponInventory.instance.IsInventoryWeapon(weapons[i].GetComponent<WeaponController>()))
            {
                ActivateWeapon(i);
                return;
            }
        }

        for (int i = weapons.Length - 1; i >= 0; i--)
        {
            if (WeaponInventory.instance.IsInventoryWeapon(weapons[i].GetComponent<WeaponController>()))
            {
                ActivateWeapon(i);
                return;
            }
        }
    }

    void ActivateWeapon(int index)
    {
        for(int i = 0;i < weapons.Length;i++) {

          weapons[i].SetActive(i == index);

        }

        currentWeaponIndex = index;
    }
}
