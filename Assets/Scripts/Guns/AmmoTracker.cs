using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoTracker : MonoBehaviour
{
    public int PistolAmmo;
    public int PistolMaxAmmo;    
    public int RifleAmmo;
    public int RifleMaxAmmo;    
    public int SniperAmmo;
    public int SniperMaxAmmo;    
    public int HeavyAmmo;
    public int HeavyMaxAmmo;

    public GameObject ActiveGun;


    // Start is called before the first frame update
    void Start()
    {
        PistolMaxAmmo = int.MaxValue;
        PistolAmmo = int.MaxValue;
        RifleMaxAmmo = 250; 
        SniperMaxAmmo = 50;
        HeavyMaxAmmo = 25;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                ActiveGun = gameObject.transform.GetChild(i).gameObject;
            }
        }

        if (ActiveGun.transform.tag == ("Pistol"))
        {
            Pistol();
        }
        if (ActiveGun.transform.tag == ("Rifle"))
        {
            Rifle();
        }
        if (ActiveGun.transform.tag == ("Sniper"))
        {
            Sniper();
        }
        if (ActiveGun.transform.tag == ("Heavy"))
        {
            Heavy();
        }
    }


    void Pistol() 
    {
        PistolAmmo = int.MaxValue;
    }
    void Rifle() 
    {

    }
    void Sniper() 
    {

    }
    void Heavy() 
    {

    }
}
