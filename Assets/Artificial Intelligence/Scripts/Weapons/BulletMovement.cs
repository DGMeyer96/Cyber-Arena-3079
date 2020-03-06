using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * 100;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
