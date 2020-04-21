using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponPosition : MonoBehaviour
{
    public GameObject gunHolder;

    void Update()
    {
        transform.position = Vector3.Slerp(transform.position, gunHolder.transform.position, 1000 * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, gunHolder.transform.rotation, 1000 * Time.deltaTime);
    }
}