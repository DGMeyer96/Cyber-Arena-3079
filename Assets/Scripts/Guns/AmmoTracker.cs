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
    public PlayerUIController playerUIController;

    // Start is called before the first frame update
    void Start()
    {
        PistolMaxAmmo = int.MaxValue;
        PistolAmmo = int.MaxValue;
        RifleMaxAmmo = 250;
        SniperMaxAmmo = 50;
        HeavyMaxAmmo = 25;
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
        playerUIController.SetAmmo(PistolAmmo, RifleAmmo, SniperAmmo, HeavyAmmo);
    }

    void Pistol()
    {
        PistolAmmo = int.MaxValue;
    }
}
