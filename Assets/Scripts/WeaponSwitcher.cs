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
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                currentWeaponIndex = i;
                ActivateWeapon(currentWeaponIndex);
            }
        }
    }

    void ActivateWeapon(int index)
    {
        for(int i = 0;i < weapons.Length;i++) {

          weapons[i].SetActive(i == index);

        }
    }
}
