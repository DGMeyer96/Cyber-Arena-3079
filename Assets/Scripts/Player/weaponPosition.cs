using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPosition : Bolt.EntityBehaviour<IBensState>
{
    public GameObject gunHolder;

    public AmmoTracker ammoTracker;
    public WeaponSwitch weaponSwitch;
    void Update()
    {
        if (entity.IsOwner && ammoTracker.enabled == false)
        {
            ammoTracker.enabled = true;
        }        
        if (entity.IsOwner && weaponSwitch.enabled == false)
        {
            weaponSwitch.enabled = true;
        }
        transform.position = Vector3.Slerp(transform.position, gunHolder.transform.position, 1000 * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, gunHolder.transform.rotation, 1000 * Time.deltaTime);
    }
}