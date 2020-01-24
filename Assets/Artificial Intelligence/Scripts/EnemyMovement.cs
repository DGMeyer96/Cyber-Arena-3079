﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public GameObject weapon;

    public GameObject[] targetpoints;
    private int locationCounter;
    private NavMeshAgent navMesh;
    public bool playerDetected = false;
    public bool isThisLoss = false;

    [SerializeField]
    private float followTimer = 5.0f;

    public GameObject player;

    //[SerializeField]
    public float waitTimer = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        locationCounter = 0;
        
        navMesh.isStopped = false;
        GoToNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerDetected)
        {
            if (!navMesh.pathPending && navMesh.remainingDistance < 0.1f)
            {
                navMesh.isStopped = true;
                GoToNextPoint();
            }
        }
        else if (playerDetected && followTimer >= 0.0f)
        {
            
            if (isThisLoss)
            {
                navMesh.isStopped = true;
                //followTimer -= Time.deltaTime;
            }
            else
            {
                navMesh.isStopped = false;
                followTimer -= Time.deltaTime;
                navMesh.destination = player.transform.position;
                if(navMesh.remainingDistance < 10.0f)
                {
                    weapon.GetComponent<EnemyAttack>().isAttacking = true;
                }
                else if(navMesh.remainingDistance > 40.0f)
                {
                    weapon.GetComponent<EnemyAttack>().isAttacking = false;
                }
            }
        }
        if(followTimer <= 0)
        {
            playerDetected = false;
            followTimer = 5.0f;
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

}
