using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public float speed = 10f;
    public Transform PlayerBody;
    public Transform weapon;
    float xRotation = 0f;

    Quaternion rotation;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Time.deltaTime prevents framerate based speed
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        //Prevents over rotation = looking backward
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //Rotate the body and camera
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        PlayerBody.Rotate(Vector3.up * mouseX);


        //use these next lines if you want to slerp gun.  gun must not be aprented to palyer.
        //this may effect root motion
        //use this below if you want weapon to slerp isntead of being rigid
        //updates the weapon rotation
        rotation = Quaternion.Lerp(weapon.rotation, transform.rotation, Time.deltaTime * speed);
        weapon.transform.localRotation = rotation;

        //update the position using the camera
        weapon.transform.position = transform.position;
    }
}
