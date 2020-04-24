using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Gun : MonoBehaviour
{
    public float Damage = 10f;
    public float Range = 100f;
    public float FireRate = 15f; //lower the number the higher the rate NEVER set to 1
    public bool IsAutomatic = false;
    public GameObject sparkattack;
    public VisualEffect hitaffect;                  //The actual visual affect itself. We are grabbing it so we can edit the direction the sparks move depending on what wall they hit
    public float spreadfactor = 0.001f;             //Example we dont want the player to fire at a wall and for it to go the opposit direction of the spark
    public static readonly string sparkdirection = "VelocityDirection";
    public VisualEffect Muzzleflash;

    private Vector3 reflection;
    private readonly float firecooldown = 1f;         //Temp cooldown of firing weapon from shot to shot works with NextTimeToFire
    private float NextTimeToFire = 0f;      //Temp cooldown of firing weapon works with fire rate and then checks its size vs firecooldown to run




    /*
    TODO:
    Fix whatever the hell this fire rate is  ->DONE<-
    Broken prefab for testing missing stuff and says I cant add anything ->DONE<-
    direction of spawning sparks ->It doesn't work as intended but I don't fucking care. I am done
    intensity of said mentioned sparks
    Rebuild the muzzle flash (again :(  )  muzzle flash is pulled because reflections really fuck with it rn
    Get respawn working
    */
    /*
    Fire rate -> the lower the number the higher the rate  
      
    */

    void Update()
    {
        if(IsAutomatic)
        {
            NextTimeToFire += Time.deltaTime + 1f / FireRate;
            if (Input.GetButton("Fire1"))
            {
             //   Muzzleflash.SendEvent("OnFire");
                Debug.Log(NextTimeToFire);

                if (NextTimeToFire > firecooldown)
                {
                    NextTimeToFire = 0;
                    Debug.Log("fires");
                    Shoot();
                }
            }
            else
            {
          //      Muzzleflash.SendEvent("OnStopFire");
            }
        }
        else  // Single fire this is ok but what if player fires as fast as they can pull trigger? More damage than full auto *MUST fix*
        {       //For now I will do it similar to the full auto check, but I question if there is a better alternative, maybe sit down and think of alternate ideas to run better
            NextTimeToFire += Time.deltaTime + 1f / FireRate;
            if (Input.GetButtonDown("Fire1"))
            {
                Debug.Log(NextTimeToFire);

                if (NextTimeToFire > firecooldown)
                {
          //          Muzzleflash.SendEvent("OnFire");
                    NextTimeToFire = 0;
                    Debug.Log("fires");
                    Shoot();
        //            Muzzleflash.SendEvent("OnStopFire");
                }
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









            Debug.DrawLine(transform.position, hit.point, Color.green, 5.0f);



            reflection = Vector3.Reflect(transform.position, hit.normal);     //Wall directions are not the same so I can't use vector.reflect, but it doesn't matter anyway because the particles are not following the vector.reflect
            
            hitaffect.SetVector3(sparkdirection, hit.normal);             //Current issue it won't let me assign my visual effect why not?
                                                                          //Previous issue resolved, now they are just giving it NaNs google searches have revealed the code is giving someone their grandma will need to research why




            Debug.Log("player position: " + transform.position + " And hit position: " + hit.normal + " And reflection direction" + reflection);


            Debug.DrawLine(hit.point, reflection, Color.cyan, 5.0f);


























            GameObject temp = Instantiate(sparkattack, hit.point, Quaternion.identity);
            Destroy(temp, 1.0f);
            if(hit.collider.GetComponent<Health>())
            {
                hit.collider.GetComponent<Health>().TakeDamage(Damage);
            }
        }
    }
}