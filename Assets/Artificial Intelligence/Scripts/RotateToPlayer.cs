using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPlayer : MonoBehaviour
{
    public GameObject enemyObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyObject.GetComponent<EnemyMovement>().playerDetected)
        {
            transform.LookAt(enemyObject.GetComponent<EnemyMovement>().player.transform);
        }
    }
}
