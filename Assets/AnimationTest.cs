using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    // Start is called before the first frame update
    Animator ani;
    Rigidbody rb;
    public float scaleLimit = 2.0f;
    public float z = 40f;
    public int count = 30;


    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ani.SetBool("Shoot", true);

        }
        else
        {
            ani.SetBool("Shoot", false);
        }
    }

}
