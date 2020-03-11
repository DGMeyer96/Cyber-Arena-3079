using UnityEngine;
using System.Collections;

public class BoltPlayerBehavior : Bolt.EntityBehaviour<IPlayerState>
{
    //public CharacterController CharController;
    //private Vector3 Velocity;
    public Camera EntityCamera;


    public override void Attached()
    {
        //CharController = gameObject.GetComponent<CharacterController>();
        state.SetTransforms(state.PlayerTransform, transform);
        //if (entity.IsOwner)
        //{
        //    EntityCamera.gameObject.SetActive(true);
        //}
    }

    private void Update()
    {
        if (entity.IsOwner && EntityCamera.gameObject.activeInHierarchy == false)
        {
            EntityCamera.gameObject.SetActive(true);
        }
    }

    public override void SimulateOwner()
    {
        /*
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //transform.right and transform.forward uses local coords instead of world coords
        Vector3 move = transform.right * x + transform.forward * z;

        CharController.Move(move * 12f * Time.deltaTime);
        */
        //applies forces on the y axis from jumping or gravity or jetpack
        //-9.81m/s * t * t
        //CharController.Move(Velocity * Time.deltaTime);

        var speed = 12f;
        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) { movement.z += 1; }
        if (Input.GetKey(KeyCode.S)) { movement.z -= 1; }
        if (Input.GetKey(KeyCode.A)) { movement.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { movement.x += 1; }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * speed * BoltNetwork.FrameDeltaTime);
        }
    }
}