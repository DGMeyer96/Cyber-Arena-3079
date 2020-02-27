using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public float FireRate = 15f;
    public float ImpactForce = 30f;

    public bool IsAutomatic = false;
    public GameObject sparkattack;
    public Camera fpsCamera;
    public VisualEffect Bulletfire;
    public static readonly string Target = "Target";
    public static readonly string Velocity = "velocity";
    public static readonly string position = "position";
    public static readonly string SpawnLocation = "SpawnLocation";

    // private Transform temptransform;


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
              //  Bulletfire.SendEvent("OnFiring");
                Shoot();
                Debug.Log("Firing Gun - Full-Auto");
            }
            else if (!(Input.GetButton("Fire1")))
            {
            //   Bulletfire.SendEvent("OnStopFiring");
            //    Bulletfire.SendEvent("OnStopBulletFire");

            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
            //    Bulletfire.SendEvent("OnFiring");
                Shoot();
                Debug.Log("Firing Gun - Semi-Auto");
            }
            else if (!(Input.GetButtonDown("Fire1")))
            {

              //  Bulletfire.SendEvent("OnStopFiring");

            }


        }


    }

    void Shoot()
    {
        //MuzzleFlash.Play();

        RaycastHit hit;

     //   Bulletfire.SendEvent("OnBulletFire");
       Vector3 newvelocity;

        //  Bulletfire.SetVector3(Velocity, fpsCamera.transform.forward * 5);

        Debug.Log("Gets here");
        if (Physics.Raycast(transform.position, fpsCamera.transform.forward, out hit,Mathf.Infinity))
        {
           // newvelocity = (transform.position. - hit.transform.position) / Time.deltaTime;
        //    Debug.Log("This is velocity" + newvelocity);
         //   Bulletfire.SetVector3(Velocity, newvelocity);
            Debug.Log(hit.transform.name);
            //temptransform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            
            newvelocity = transform.InverseTransformPoint(hit.point);
          //  Bulletfire.SetVector3(Target, newvelocity);
            Bulletfire.SetVector3(SpawnLocation, newvelocity);
            Bulletfire.SendEvent("OnHit");
            Debug.Log("This is Target" + newvelocity);
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
