using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public GameObject[] targetpoints;
    private int locationCounter;
    private NavMeshAgent navMesh;

    //[SerializeField]
    public float waitTimer = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        locationCounter = 0;
        Debug.Log("Starting");
        navMesh.isStopped = false;
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if(!navMesh.pathPending && navMesh.remainingDistance < 0.1f)
        {
            navMesh.isStopped = true;
            GoToNextPoint();
        }
    }

    void GoToNextPoint()
    {
        if (targetpoints.Length == 0)
            return;
        //if(!patrolling)
        //{
        //    waitTimer -= Time.deltaTime;
        //    if(waitTimer < 0.0f)
        //    {
        //        waitTimer = 2.0f;
        //    }
        //    return;
        //}
        if (navMesh.isStopped)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer < 0.0f)
            {
                waitTimer = 2.0f;
                navMesh.isStopped = false;
                navMesh.destination = targetpoints[locationCounter].transform.position;

                locationCounter = (locationCounter + 1) % targetpoints.Length;
            }
            return;
        }
            navMesh.destination = targetpoints[locationCounter].transform.position;

            locationCounter = (locationCounter + 1) % targetpoints.Length;
    }

    //void OnCollisionEnter(Collision collider)
    //{
    //    if(collider.gameObject.tag == "TargetPoint")
    //    {
    //        Debug.Log("Target Reached");
    //        patrolling = false;
            
    //    }
    //}
}
