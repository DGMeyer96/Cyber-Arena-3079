using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public int SelectedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int PreviousSelectedWeapon = SelectedWeapon;
        //if (Input.GetAxis("Mouse ScrollWheel") > 0) 
        //{
        //    if (SelectedWeapon >= transform.childCount - 1)
        //    {
        //        SelectedWeapon = 0;
        //    }
        //    SelectedWeapon++;
        //}        
        //if (Input.GetAxis("Mouse ScrollWheel") < 0) 
        //{
        //    if (SelectedWeapon <= transform.childCount - 1)
        //    {
        //        SelectedWeapon = 0;
        //    }
        //    SelectedWeapon--;
        //}
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }        
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            SelectedWeapon = 1;
        }        
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            SelectedWeapon = 2;
        }        
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            SelectedWeapon = 3;
        }
        if (PreviousSelectedWeapon != SelectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon() 
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}
