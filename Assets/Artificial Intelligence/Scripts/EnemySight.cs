using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{
    public GameObject enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        //enemyObject = this.transform.parent.gameObject;
        //if (!enemyObject)
        //    Debug.Log("No owner");
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            enemyObject.GetComponent<EnemyMovement>().player = other.transform.gameObject;
            enemyObject.GetComponent<EnemyMovement>().playerDetected = true;
            //enemyObject.GetComponentInChildren<EnemyAttack>().isAttacking = true;
        }
    }
}
