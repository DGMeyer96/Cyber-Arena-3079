using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public float FireRate = 15f;
   // public float ImpactForce = 30f;

    public bool IsAutomatic = false;
    public GameObject sparkattack;
    public float spreadfactor = 0.001f;
    private float NextTimeToFire = 0f;


    void Update()
    {
        if(IsAutomatic)
        {
            if (Input.GetButton("Fire1") && Time.time >= NextTimeToFire)
            {
                NextTimeToFire = Time.time + 1f / FireRate;
                Shoot();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector3 direction = transform.forward;
        direction.x += Random.Range(-spreadfactor, spreadfactor);
        direction.y += Random.Range(-spreadfactor, spreadfactor);
        direction.z += Random.Range(-spreadfactor, spreadfactor);
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit,Range))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 1.0f);            
            Debug.Log("This is Target : " + hit.collider.name);
            GameObject temp = Instantiate(sparkattack, hit.point, Quaternion.identity);
            Destroy(temp, 1.0f);
            if(hit.collider.GetComponent<Health>())
            {
                hit.collider.GetComponent<Health>().TakeDamage(Damage);
            }
        }
    }
}