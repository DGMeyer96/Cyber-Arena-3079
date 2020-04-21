using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupReset : MonoBehaviour
{
    public bool respawn;
    public float respawntimer = 15;
    public float ctimer = 0;
    public GameObject holder;

    void Update()
    {
        if (holder.activeInHierarchy == false)
        {
            respawn = true;
            GetComponent<SphereCollider>().enabled = false;
        }
        if (holder.activeInHierarchy == true)
        {
            respawn = false;
        }
        if (respawn)
        {
            ctimer += Time.deltaTime;
        }
        if (ctimer > respawntimer)
        {
            GetComponent<SphereCollider>().enabled = true;
            ctimer = 0;
            respawn = false;
            holder.SetActive(true);
        }
    }
}
