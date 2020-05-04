using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public GameObject enemyObject;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyObject.GetComponent<EnemyMovement>().player = other.transform.gameObject;
            enemyObject.GetComponent<EnemyMovement>().playerDetected = true;
        }
    }
}
