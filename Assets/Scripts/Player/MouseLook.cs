using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float MouseSensitivity = 100f;
    public float speed = 100f;
    public Transform PlayerBody;
    public Transform PlayerHolder;
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



        //updates the weapon position
        rotation = Quaternion.Lerp(weapon.rotation, transform.rotation, Time.deltaTime * speed);
        weapon.transform.localRotation = rotation;

        //update the position using the holders
        weapon.transform.position = transform.position;
    }

    void LateUpdate()
    {

    }
}
