using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverFinding : MonoBehaviour
{
    public GameObject enemyObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enemyObject.GetComponent<EnemyMovement>().playerDetected)
        {
            if(other.gameObject.layer == 8)
            {
                Debug.Log("Cover Found.");
                enemyObject.GetComponent<EnemyMovement>().takeCover = true;
                enemyObject.GetComponent<EnemyMovement>().coverLocation = other.gameObject.transform.position;
            }
        }
    }
}
