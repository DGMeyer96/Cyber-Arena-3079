using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking = false;
    public float attackTimer = 3.0f;
    public GameObject enemyObject;
    public GameObject bulletSpawn;
    public float reloadSpeed = 0;

    public GameObject bullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isAttacking)
        {
            attackTimer -= 1.0f;
            //if(enemyObject.GetComponent<EnemyMovement>().isThisLoss == false)
            //    enemyObject.GetComponent<EnemyMovement>().isThisLoss = true;
            reloadSpeed -= Time.deltaTime;

                Fire();
        }

        if(attackTimer <= 0)
        {
            //enemyObject.GetComponent<EnemyMovement>().isThisLoss = false;
            isAttacking = false;
            attackTimer = 3.0f;
        }
    }

    void Fire()
    {        
        if (reloadSpeed <= 0)
        {
            //Debug.Log("Firing");
            GameObject bulletClone = Instantiate(bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
            reloadSpeed = 1.0f;
        }
    }
}
