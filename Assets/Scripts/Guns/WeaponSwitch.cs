using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class WeaponSwitch : Bolt.EntityEventListener<IBensState>
{
    public GameObject[] WeaponObjects;
    public AmmoTracker AmmoTracker;
    public bool useable;
    public int CurrentAmmo;
    public int SelectedWeapon = 0;
    public int NumOfWeapons;

    void Start()
    {
        SelectWeapon();
        Attached();
        Weaponstart();
    }

    public override void Attached()
    {
        if (entity.IsOwner)
        {
            for (int i = 0; i < state.WeaponArray.Length; ++i)
            {
                state.WeaponArray[i].WeaponId = i;
            }
            state.WeaponActiveIndex = 0;
        }
        state.SetTransforms(state.WeaponTransform, transform);
        state.AddCallback("WeaponActiveIndex", WeaponActiveIndexChanged);
    }

    public void FixedUpdate()
    {
        state.SetTransforms(state.WeaponTransform, transform);
    }

    public override void SimulateOwner()
    {
        NumOfWeapons = transform.childCount;

        int PreviousSelectedWeapon = SelectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (SelectedWeapon >= NumOfWeapons - 1)
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (SelectedWeapon == 0)
            {
                SelectedWeapon = NumOfWeapons - 1;
            }
            else
            {
                SelectedWeapon--;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && NumOfWeapons >= 2)
        {
            SelectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && NumOfWeapons >= 3)
        {
            SelectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && NumOfWeapons >= 4)
        {
            SelectedWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && NumOfWeapons >= 5)
        {
            SelectedWeapon = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) && NumOfWeapons >= 6)
        {
            SelectedWeapon = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && NumOfWeapons >= 7)
        {
            SelectedWeapon = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) && NumOfWeapons >= 8)
        {
            SelectedWeapon = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) && NumOfWeapons >= 9)
        {
            SelectedWeapon = 8;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && NumOfWeapons >= 0)
        {
            SelectedWeapon = 9;
        }
        if (PreviousSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        useable = false;
        while (useable == false)
        {
            if (gameObject.transform.GetChild(SelectedWeapon).tag == ("Pistol"))
            {
                CurrentAmmo = AmmoTracker.PistolAmmo;
                useable = true;
            }
            else if (gameObject.transform.GetChild(SelectedWeapon).tag == ("Rifle"))
            {
                CurrentAmmo = AmmoTracker.RifleAmmo;
            }
            else if (gameObject.transform.GetChild(SelectedWeapon).tag == ("Sniper"))
            {
                CurrentAmmo = AmmoTracker.SniperAmmo;
            }
            else if (gameObject.transform.GetChild(SelectedWeapon).tag == ("Heavy"))
            {
                CurrentAmmo = AmmoTracker.HeavyAmmo;
            }

            if (CurrentAmmo == 0)
            {
                if (SelectedWeapon == 0)
                {
                    SelectedWeapon = 0;
                }
                else
                {
                    SelectedWeapon--;
                }
            }
            else
            {
                useable = true;
            }
        }
        state.WeaponActiveIndex = SelectedWeapon;
    }

    public void WeaponActiveIndexChanged()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                int objectId = state.WeaponArray[state.WeaponActiveIndex].WeaponId;
                WeaponObjects[objectId].SetActive(true);
            }
            else
            {
                WeaponObjects[i].SetActive(false);
            }
            i++;
        }
    }    
    
    public void Weaponstart()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == 0)
            {
                weapon.gameObject.SetActive(true);
                int objectId = state.WeaponArray[0].WeaponId;
                WeaponObjects[0].SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
                WeaponObjects[i].SetActive(false);
            }
            i++;
        }
    }
}