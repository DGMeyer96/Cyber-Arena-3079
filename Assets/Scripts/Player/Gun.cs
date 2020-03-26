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




    private float NextTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(IsAutomatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1f / FireRate;
                Shoot();
                Debug.Log("Firing Gun - Full-Auto");
            }
            else if (!(Input.GetButton("Fire1")))
            {

            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
                Debug.Log("Firing Gun - Semi-Auto");
            }
            else if (!(Input.GetButtonDown("Fire1")))
            {
            }


        }


    }

    void Shoot()
    {

        RaycastHit hit;

       Vector3 newvelocity;


        Debug.Log("Gets here");
        if (Physics.Raycast(transform.position, fpsCamera.transform.forward, out hit,Mathf.Infinity))
        {
            Debug.Log(hit.transform.name);
            //temptransform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            
            newvelocity = transform.InverseTransformPoint(hit.point);
            Debug.Log("This is Target" + hit.point);
            Debug.DrawLine(transform.position, hit.point, Color.green);
            Enemy enemy = hit.transform.GetComponent<Enemy>();//This will not work wtf change to tags
            GameObject temp = Instantiate(sparkattack, hit.point, Quaternion.identity);
            Destroy(temp, 1.0f);


/*          So make health scripts and actually do this
            if (enemy != null)
            {
                enemy.TakeDamage(Damage);
            }

            if(hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * ImpactForce);
            }
            */
        }
    }
}
