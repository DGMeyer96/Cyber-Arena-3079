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

    public Transform GroundCheck;
    public LayerMask GroundMask;
    //Radius for checking for floor
    public float GroundDistance = 0.4f;

    //Used for gravity
    Vector3 Velocity;

    bool IsGrounded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check in a sphere if the floor is in range, like a collider check
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);

        //Check if the player is on the ground but was falling at some point
        if(IsGrounded && Velocity.y < 0)
        {
            //Not 0 so we can force the player onto the ground
            Velocity.y = -0.5f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //transform.right and transform.forward uses local coords instead of world coords
        Vector3 move = transform.right * x + transform.forward * z;

        CharController.Move(move * MoveSpeed * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && IsGrounded)
        {
            Debug.Log("Jumping");
            //v = Sqrt(h * -2 * g)
            Velocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
        }

        //-9.81m/s * t
        Velocity.y += Gravity * Time.deltaTime;
        //-9.81m/s * t * t
        CharController.Move(Velocity * Time.deltaTime);
    }
}
