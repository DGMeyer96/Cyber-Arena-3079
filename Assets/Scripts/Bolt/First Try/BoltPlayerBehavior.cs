using UnityEngine;
using System.Collections;

public class BoltPlayerBehavior : Bolt.EntityBehaviour<IBensState>
{
    public Camera EntityCamera;
    public GameObject test;
    public CharacterController CharController;
    public Transform[] spawns;

    public AmmoTracker ammoTracker;
    public Player playerscript;
    public PickupReset pickupReset;

    private float MoveSpeed;
    public float MovespeedControler = 12f;
    public float JumpHeight = 3f;
    public float SlideSpeed = 3000f;//controls slide speed
    private float TempSlideSpeed;
    public float slidedec = 10f;
    public float jetpackfuel; //current fuel in the jetpack
    public float jetpackfuelmax = 5; 
    public float jetpackmaxVel = 16; 
    public float jetpackAcc = 8; 
    public float DblJump;//tracks how many jumps the palyer has done
    private float JumpTimer;//sets a timer before the palyer can double jump
    private float fallmult = 2.5f; //increase gravity pull for better feel
    public float Gravity = -9.81f;
    public float GroundDistance = 0.4f;
    public float SlopeRayLength = 1f;
    public float SlopeForce = 1f;
    private float CrouchTimer;//sets a timer before the palyer can double jump
    private float height;//height of the character controller
    private float radius;//radius of the character controller 
    private float poweruptimer;

    public Transform GroundCheck;
    public LayerMask GroundMask;
    public LayerMask VualtLayer;
    public LayerMask WallLayer;
    public LayerMask EverythingButPlayer;
    private Vector3 SlideForce; //Force for sliding;
    private Vector3 slideDIR;
    private Vector3 Velocity;
    private Vector3 Vaultpos;//new pos after vualting
    private Vector3 Climbpos;

    public bool IsCrouching; //keeps track if the palyer is crouching or not
    public bool CanStand;//wont let palyer stands if something is blocking him
    public bool sliding;
    private bool firstslide;
    public bool IsTouchingWall;
    public bool IsRoomForClimb;
    public bool IsGrounded;
    public bool Jumping;
    public bool IsOnSlope;
    public bool cancrouch;
    public bool powerup;

    public bool CanJmp;
    public float JmpCount;

    public override void Attached()
    {
        CharController = GetComponent<CharacterController>();
        state.SetTransforms(state.PlayerTransform, transform);
        jetpackfuel = 5f;
        height = CharController.height;
        radius = CharController.radius;
        IsCrouching = false;
        CanJmp = true;
    }

    private void Update()
    {
        //Debug.Log(playerscript.health + "  -  " + playerscript.shield);
        //Debug.Log(playerscript.maxhealth + "  -  " + playerscript.maxshield);
        //if (entity.IsOwner && EntityCamera.gameObject.activeInHierarchy == false)
        //{
        //    EntityCamera.gameObject.SetActive(true);
        //} 
        if (entity.IsOwner && test.gameObject.activeInHierarchy == false)
        {
            test.gameObject.SetActive(true);
        }
        if (entity.IsOwner && CharController.enabled == false)
        {
            CharController.enabled = true;
        }
        Jump();
        Crouch();//accepts continuous input for sliding and crouching
        //if (!IsCrouching)
        //{
        //    Vualt();
        //    Climb();//used for ledge detection
        //}
    }

    void FixedUpdate()
    {
        //Check in a sphere if the floor is in range, like a collider check
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask, QueryTriggerInteraction.Ignore);
        if (!IsGrounded)
        {
            IsGrounded = CharController.isGrounded;
        }

        if (IsGrounded)
        {
            Jumping = false;
            JmpCount = 0;
        }

        JetPack();//accepst continuous input for jetpack
        Movement();//executes movement force
        Slide();//exectues sliding force     //add a slide cooldown
        if (!sliding)
        {
            OnSlope();//executes additional gravity to cause palyer to hug slopes
        }
    }

    //public void SpawnPlayer()
    //{
    //    Transform tmp = spawns[Random.Range(0, spawns.Length)];
    //    this.transform.position = tmp.position;
    //    this.transform.rotation = tmp.rotation;
    //}

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && CanJmp && JmpCount < 2)
        {
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            Jumping = true;
            CanJmp = false;
        }
        if (Input.GetButtonUp("Jump") && !CanJmp && JmpCount < 2)
        {
            CanJmp = true;
            JmpCount++;
        }
    }

    /*
    void Vualt()
    {
        Vector3 pos = transform.position + (Vector3.down * height / 3f);
        if (Physics.SphereCast(pos, radius, transform.forward, out var hit, 2f, VualtLayer) || Physics.SphereCast(pos + (Vector3.up * height / 2), radius, transform.forward, out hit, 2f, VualtLayer))//wall hit
        {
            //Debug.DrawRay(pos, transform.forward * 5, Color.green);
            //Debug.DrawRay(pos + (Vector3.up * height / 2), transform.forward * 5, Color.green);
            Vector3 posdown = hit.point + (Vector3.up * height * 2);
            if (Physics.SphereCast(posdown, .1f, Vector3.down, out var hit2, VualtLayer))//top of the wall found
            {
                //Debug.DrawRay(posdown, Vector3.down * 5, Color.gray);
                Vector3 pos2 = transform.position + (Vector3.down * height / 3f) + (transform.forward * 7f);
                //Debug.DrawRay(pos2, transform.forward * -1 * 6.5f, Color.red);
                //Debug.DrawRay(pos2 + (Vector3.up * height / 2), transform.forward * -1 * 6.5f, Color.red);

                if (Physics.Raycast(transform.position + (Vector3.up * height / 2), transform.forward, 3f))//check if there is something blocking the palyers view.  otherwise he cant vualt
                {
                    //Debug.Log("WALL");
                }
                else if (Physics.SphereCast(pos2, radius, transform.forward * -1, out var hit3, 6.5f, VualtLayer) || Physics.SphereCast(pos2 + (Vector3.up * height / 2), radius, transform.forward * -1, out hit3, 6.5f, VualtLayer))//back of the wall found
                {

                    float dist = Vector3.Distance(hit2.point, hit3.point);//find the width of the wall
                    if (dist > 3f || dist < .6f)
                    {
                        //Debug.Log("noVualt");
                    }
                    else
                    {
                        //Debug.Log(dist);
                        Vaultpos = hit3.point;
                        Vaultpos.y = transform.position.y;
                        Vaultpos += (transform.forward * radius * 1.3f);
                        Debug.DrawRay(Vaultpos, transform.up * 5, Color.blue);//this line represents where the palyer will transport when vualting
                        //Debug.Log("old pos = " + transform.position);
                        //Debug.Log("new pos = " + Vaultpos);
                        //Debug.Log("VROOM");
                    }
                }
                else
                {
                    //Debug.Log("Nope");
                }
            }
            else
            {
                //Debug.Log("Wall");
            }
        }
    }

    void Climb()
    {
        Vector3 pos = transform.position + (Vector3.up * height / 3f) + (transform.forward * radius / 2f);

        if (Physics.SphereCast(pos, 0.2f, transform.forward, out var hit, 3f, WallLayer))//wall hit
        {
            //Debug.DrawRay(pos, transform.forward * 5, Color.green);

            Vector3 posdown = hit.point + (Vector3.up * height * 2);
            //Debug.DrawRay(posdown, Vector3.down * 5, Color.gray);
            if (Physics.SphereCast(posdown, .1f, Vector3.down, out var hit2))//top of the wall found
            {
                //Debug.DrawRay(hit2.point + (Vector3.up * .3f), transform.forward * 5, Color.green);
                if (Physics.Raycast(hit2.point + (Vector3.up * .3f), transform.forward, 1f))//if there is something right above the top of the wall then palyer cant get here
                {
                    //Debug.Log("WALL");
                }
                else
                {
                    if (hit2.point.y < hit.point.y)//wall is present cause first raycast detected wall but the second raycast did not.  this means it is to tall tob e detected
                    {
                        //Debug.Log("Wall");
                    }
                    else //the area should be open
                    {
                        Vector3 pos1 = hit2.point + (Vector3.up * (height - radius));
                        Vector3 pos2 = hit2.point + (Vector3.up * radius);
                        //Debug.DrawRay(pos2, transform.forward * 5, Color.green);
                        Physics.SphereCast(pos1, radius, Vector3.down, out var hit3, height - (radius * 2));
                        if (hit.point.y < pos2.y)//send cast down to see if the ledge is open send another cast up to see if there is a wall above you
                        {
                            if (!Physics.Raycast(transform.position, transform.up, height * 2))//see if there is a block above
                            {
                                //Debug.DrawRay(transform.position + (Vector3.up * (height * 2)), transform.forward * 3, Color.blue);
                                if (!Physics.SphereCast(transform.position + (Vector3.up * (height * 2)), radius, transform.forward, out var hit4, 3f))//room in front
                                {
                                    //Debug.Log("WROOM");
                                    Climbpos = hit2.point;
                                    Climbpos.y = Climbpos.y + height / 2;
                                    Climbpos += (transform.forward * radius * 1.3f);
                                    //Debug.DrawRay(Climbpos, transform.up * height / 2, Color.blue);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    */

    void JetPack()
    {
        //Check if the player is on the ground but was falling at some point
        if (IsGrounded && Velocity.y < 0)
        {
            //Not 0 so we can force the player onto the ground
            Velocity.y = -0.5f;
            if (jetpackfuel < jetpackfuelmax) //recharge jetpack
            {
                jetpackfuel += Time.deltaTime;
            }
        }

        if (Input.GetAxis("JetPack") > 0 && Input.GetAxis("Jump") > 0  && jetpackfuel > 0)
        {
            if (Velocity.y >= 0 && Velocity.y < jetpackmaxVel) //if velocity >= 0 apply a constant force until velocity is equal to 10
            {
                Velocity.y += jetpackAcc * Time.deltaTime;
            }
            else if (Velocity.y <= 0) //if velocity is < 0 apply a force that will cancel out gravity.  this creates a drag effect
            {
                Velocity.y += 10f * Time.deltaTime;
            }

            jetpackfuel -= Time.deltaTime;
        }
        else //if jetpack is not in use or is out of gas then player will fall 
        {
            //-9.81m/s * t
            //Velocity.y += Gravity * Time.deltaTime;
            Velocity += Vector3.up * Gravity * (fallmult - 1) * Time.deltaTime; // increases fall gravity for better feel
        }
    }

    void Crouch()
    {
        if (IsCrouching)
        {
            MoveSpeed = MovespeedControler / 2;
        }
        if (!IsCrouching)
        {
            MoveSpeed = MovespeedControler;
        }

        if (Physics.Raycast(transform.position, transform.up, out var hit, 3, EverythingButPlayer))//check to make sure there is nothing over the character
        {
            CanStand = false;
        }
        else
        {
            CanStand = true;
        }
        if (Physics.Raycast(transform.position, -transform.up, out var hit2, ((height / 2) + 1), EverythingButPlayer))//check to make sure there is something below the player
        {
            cancrouch = true;
        }
        else
        {
            cancrouch = false;
            IsGrounded = false;
        }
        if (Input.GetButtonDown("Slide") && sliding == false && (!IsGrounded || Jumping))//player will slide in air
        {
            sliding = true;
            firstslide = true;
            CrouchTimer = 0;
        }
        if ((Input.GetButtonUp("Crouch") || Input.GetButtonUp("Slide")) && CanStand)//if there is nothing over the character he can stand back up
        {
            IsCrouching = false;
            sliding = false;
            CharController.height = height;
            CrouchTimer = 0;
        }
        if (Input.GetButtonDown("Slide") && sliding == false && cancrouch && !Jumping && IsGrounded)//player will slide
        {
            IsCrouching = true;
            CharController.height = height / 2;
            sliding = true;
            firstslide = true;
            CrouchTimer = 0;
        }
        if (Input.GetButtonDown("Crouch") && !IsCrouching && cancrouch && !Jumping && IsGrounded)//player will crouch
        {
            //Debug.Log("Crouch");
            IsCrouching = true;
            CharController.height = height / 2;
            CrouchTimer = 0;
        }
    }
    void Slide()
    {
        if (sliding)//want to add behavior where slide will keep going and increase if going down an incline
        {
            if (firstslide)//initial slide speed
            {
                slideDIR = transform.forward;//saves initial direction for slide
                TempSlideSpeed = SlideSpeed;
                SlideForce = slideDIR * SlideSpeed * Time.deltaTime;
                firstslide = false;
            }
            if (TempSlideSpeed > 0)//slowly decreases sliding speed: fake friction
            {
                TempSlideSpeed = TempSlideSpeed - slidedec;
                SlideForce = slideDIR * TempSlideSpeed * Time.deltaTime;
            }
            else if (IsGrounded)
            {
                SlideForce.y = 0;
            }
            CharController.Move(SlideForce * Time.deltaTime);
        }

        if (TempSlideSpeed <= 0)
        {
            sliding = false;
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //transform.right and transform.forward uses local coords instead of world coords
        Vector3 move = transform.right * x + transform.forward * z;

        CharController.Move(move * MoveSpeed * Time.deltaTime);

        //applies forces on the y axis from jumping or gravity or jetpack
        //-9.81m/s * t * t
        CharController.Move(Velocity * Time.deltaTime);
    }

    void OnSlope()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 FowardMove = transform.forward * z;
        Vector3 rightmvoe = transform.right * x;

        if (Jumping || Input.GetAxis("Jump") != 0)
        {
            IsOnSlope = false;
            Jump();
            return;
        }

        if (Physics.Raycast(transform.position, Vector3.down, out var hit, height / 2 * SlopeRayLength))
        {
            if (hit.normal != Vector3.up)
            {
                IsOnSlope = true;
            }
        }

        if (!IsGrounded && sliding && IsOnSlope)//increase gravity alot so slide can hug the ground
        {
            SlideForce.y -= 2000 * Time.deltaTime;
        }

        if ((z != 0 || x != 0) && IsOnSlope)
        {
            CharController.Move(Vector3.down * height / 2 * SlopeForce * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit");
        if (other.gameObject.tag == "Ammo")
        {
            if (ammoTracker.RifleAmmo < ammoTracker.RifleMaxAmmo || ammoTracker.HeavyAmmo < ammoTracker.HeavyMaxAmmo || ammoTracker.SniperAmmo < ammoTracker.SniperMaxAmmo)
            {
                other.GetComponent<PickupReset>().holder.SetActive(false);
                ammoTracker.RifleAmmo += 50;
                ammoTracker.HeavyAmmo += 5;
                ammoTracker.SniperAmmo += 10;
            }
        }       
        if (other.gameObject.tag == "Shield")
        {
            if (playerscript.shield < playerscript.maxshield)
            {
                other.GetComponent<PickupReset>().holder.SetActive(false);
                playerscript.shieldPlayer(10);
            }
        }        
        if (other.gameObject.tag == "Health")
        {
            if (playerscript.health < playerscript.maxhealth)
            {
                other.GetComponent<PickupReset>().holder.SetActive(false);
                playerscript.HealPlayer(20);
            }
        }        
        if (other.gameObject.tag == "Invulnerability")
        {
            other.GetComponent<PickupReset>().holder.SetActive(false);
            powerup = true;
        }
    }
}