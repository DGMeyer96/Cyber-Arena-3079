using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    public float scaleLimit = 2.0f;
    public float z = 40f;
    public int count = 30;
    public Transform spawnPoint;
    private Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            for (int i = 0; i < count; ++i)
            {
                shootSpray();
            }

        }
    }

    void shootSpray()
    {
        //float randomRadius = scaleLimit;
        float randomRadius = Random.Range(0, scaleLimit);
        float randomAngle = Random.Range(0, 2 * Mathf.PI);

        Vector3 direction = new Vector3(
            randomRadius * Mathf.Cos(randomAngle),
            randomRadius * Mathf.Sin(randomAngle),
            z);

        direction = spawnPoint.TransformDirection(direction.normalized);
        //Debug.DrawRay(spawnPoint.position, direction * 5);

        Ray ray = new Ray(spawnPoint.position, direction);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(spawnPoint.position, hit.point);
        }
    }
}
