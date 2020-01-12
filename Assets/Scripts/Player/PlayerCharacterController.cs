using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : MonoBehaviour
{
    public CharacterController CharController;

    //How fast character moves
    public float MoveSpeed = 12f;
    //Gravity
    public float Gravity = -9.81f;
    public float JumpHeight = 3f;

    public float SlideSpeed = 3000f;
    public float slidedec = 10f;

    //public Transform GroundCheck;
    //Radius for checking for floor
    //public float GroundDistance = 0.4f;
    //public LayerMask GorundMask;

    public float jetpackfuel; //current fuel in the jetpack

    private float DblJump;//tracks how many jumps the palyer has done
    private float JumpTimer;//sets a timer before the palyer can double jump

    //Used for gravity
    Vector3 Velocity;

    public bool IsGrounded;

    private float fallmult = 2.5f; //increase gravity pull for better feel

    public bool IsCrouching; //keeps track if the palyer is crouching or not
    private float height;
    public bool CanStand;//wont let palyer stands if something is blocking him
    private float CrouchTimer;//sets a timer before the palyer can double jump


    Vector3 SlideForce; //Force for sliding;
    public bool sliding;
    private bool firstslide;
    private float TempSlideSpeed;
    private Vector3 slideDIR;



    // Start is called before the first frame update
    void Start()
    {
        jetpackfuel = 10f;
        height = CharController.height;
        IsCrouching = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check in a sphere if the floor is in range, like a collider check
        //IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask, QueryTriggerInteraction.Ignore);
        IsGrounded = CharController.isGrounded;

        //handles Moving ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //transform.right and transform.forward uses local coords instead of world coords
        Vector3 move = transform.right * x + transform.forward * z;

        CharController.Move(move * MoveSpeed * Time.deltaTime);

        //handles jumping ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //reset double jump if the ground is touched
        if (IsGrounded && DblJump == 2)
        {
            DblJump = 1;
            JumpTimer = 0;
        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            //v = Sqrt(h * -2 * g)
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);

            DblJump = 1;
        }

        if (Input.GetAxis("Jump") > 0 && !IsGrounded && DblJump == 1 && Input.GetAxis("Sprint") == 0 && JumpTimer > .3f) // if jumping in air and not using jetpack
        {
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            DblJump = 2;
        }

        if (DblJump == 1 && JumpTimer < .5f && !IsGrounded)
        {
            JumpTimer += Time.deltaTime;
        }

        //handles jetpack ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Check if the player is on the ground but was falling at some point
        if (IsGrounded && Velocity.y < 0)
        {
            //Not 0 so we can force the player onto the ground
            Velocity.y = -0.5f;
            if (jetpackfuel < 10f) //recharge jetpack
            {
                jetpackfuel += Time.deltaTime;
            }
        }

        if (Input.GetAxis("Jump") > 0 && Input.GetAxis("Sprint") > 0 && jetpackfuel > 0)
        {
            if (Velocity.y >= 0 && Velocity.y < 10) //if velocity >= 0 apply a constant force until velocity is equal to 10
            {
                Velocity.y += 3f * Time.deltaTime;
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

        //applies forces on the y axis from jumping or gravity or jetpack////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //-9.81m/s * t * t
        CharController.Move(Velocity * Time.deltaTime);

        //Optional movement systems /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        Crouch();//accepts input for sliding and crouching
        Slide();//exectues sliding
        Vualt();
        Climb();
    }

    void Crouch() {//choppy returning to max hieght is instant and feels like teleporting
        if (IsGrounded)
        {
            if (Input.GetAxis("Crouch") > 0 && Input.GetAxis("Sprint") > 0 && !IsCrouching && CrouchTimer >= .3f && sliding == false)//player will slide
            {
                IsCrouching = true;
                CharController.height = height / 2;
                sliding = true;
                firstslide = true;
                CrouchTimer = 0;
            }

            if (Input.GetAxis("Crouch") > 0 && !IsCrouching && CrouchTimer >= .3f)//player will crouch
            {
                Debug.Log("Crouch");
                IsCrouching = true;
                CharController.height = height / 2;
                CrouchTimer = 0;
            }

            else if (Input.GetAxis("Crouch") > 0 && IsCrouching && CanStand && CrouchTimer >= .2f)//if there is nothing over the character he can stand back up
            {
                IsCrouching = false;
                sliding = false;
                CharController.height = height;
                CrouchTimer = 0;
            }

            if (CrouchTimer < .5f && IsGrounded)
            {
                CrouchTimer += Time.deltaTime;
            }
        }

        if (Physics.Raycast(transform.position, transform.up, out var hit, 3))//check to make sure there is nothing over the character
        {
            CanStand = false;
        }
        else 
        {
            CanStand = true;
        }

    }

    void Slide() {
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
            if (!IsGrounded)//increase gravity alot so slide can hug the ground
            {
                SlideForce.y -= 2000 * Time.deltaTime;
            }
            else if (IsGrounded)
            {
                SlideForce.y = 0;
            }
            Debug.Log(SlideForce);
            CharController.Move(SlideForce * Time.deltaTime);
        }

        if (TempSlideSpeed <= 0)
        {
            sliding = false;
        }
    }

    void Vualt() {

    }

    void Climb(){
    
    }
}
