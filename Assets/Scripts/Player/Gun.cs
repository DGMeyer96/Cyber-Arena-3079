using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public float FireRate = 15f;
    public float ImpactForce = 30f;

    public bool IsAutomatic = false;

    public Camera fpsCamera;
    public VisualEffect Bulletfire;
    public static readonly string Target = "Target";
    public static readonly string Velocity = "velocity";

    //public GameObject ImpactEffect;

    private float NextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(IsAutomatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1f / FireRate;
                Bulletfire.SendEvent("OnFiring");
                Shoot();
                Debug.Log("Firing Gun - Full-Auto");
            }
            else if (!(Input.GetButton("Fire1")))
            {
               Bulletfire.SendEvent("OnStopFiring");
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Bulletfire.SendEvent("OnFiring");
                Shoot();
                Debug.Log("Firing Gun - Semi-Auto");
            }
            else if (!(Input.GetButtonDown("Fire1")))
            {
                Bulletfire.SendEvent("OnStopFiring");
            }
        }
        

    }

    void Shoot()
    {
        //MuzzleFlash.Play();

        RaycastHit hit;

        Bulletfire.SendEvent("OnBulletFire");
        Bulletfire.SetVector3(Velocity, fpsCamera.transform.forward * 5);


        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, Range))
        {
            Debug.Log(hit.transform.name);
            Bulletfire.SetVector3(Target, hit.transform.position);
            Debug.Log("This is direction" + fpsCamera.transform.forward);
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            

            if(enemy != null)
            {
                enemy.TakeDamage(Damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }

            //GameObject impactGO = Instantiate(ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //9Destroy(impactGO, 2f);
        }
    }
}
