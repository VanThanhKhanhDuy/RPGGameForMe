using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : Singleton<WeaponsManager>
{
    public GameObject oneHandedAxe;
    public GameObject twoHandedAxe;
    public GameObject mug;
    private GameObject currentWeapon;

    private void Start()
    {
        DisableAllWeapons();
    }

    private void Update()
    {
        ChangeWeapon();
        
    }
    private void DisableAllWeapons()
    {
        oneHandedAxe.SetActive(false);
        twoHandedAxe.SetActive(false);
        mug.SetActive(false);
    }
    private void EnableWeapon(GameObject weaponToEnable)
    {
        DisableAllWeapons();
        weaponToEnable.SetActive(true);
        currentWeapon = weaponToEnable;
    }
    private void ChangeWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EnableWeapon(oneHandedAxe);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EnableWeapon(twoHandedAxe);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EnableWeapon(mug);
        }
    }

    public GameObject GetCurrentWeapon()
    {
        return currentWeapon;
    }
}
