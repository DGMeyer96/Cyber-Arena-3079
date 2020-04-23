using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class Health : MonoBehaviour
{
    public float HealthAmount = 50f;
    public int randomSpawn;
    public GameObject temptest;
    void Start()
    {
        temptest = GameObject.Find("SpawnBitch");
    }
    public void TakeDamage(float damageTaken)
    {
        HealthAmount -= damageTaken;
        Debug.Log("Health is : " + HealthAmount);
        if (HealthAmount <= 0)
        {
            randomSpawn = Random.Range(0, 7);
            Death();
        }
    }
    //TODO I need to have it respawn rather than delete self
    void Death()
    {
        // gameObject.SetActive(false);
        //Destroy(gameObject);
        Debug.Log("I SWEAR TO GOD THIS BETTER COME BACK RIGHT" + temptest.GetComponent<MP_Level_1_Spawn_Data>().spawnPositions[randomSpawn]);

        Vector3 spawnposition = temptest.GetComponent<MP_Level_1_Spawn_Data>().spawnPositions[randomSpawn];
        Debug.Log("I SWEAR TO GOD THIS BETTER COME BACK RIGHT" + spawnposition);

        Quaternion spawnrotation = temptest.GetComponent<MP_Level_1_Spawn_Data>().spawnRotations[randomSpawn];
        //BoltNetwork.Instantiate(BoltPrefabs.Player, spawnposition, spawnrotation);
        gameObject.transform.position = spawnposition;
        Debug.Log("Hello?");
        Debug.Log("Should be correct" + gameObject.transform.position);
        gameObject.transform.rotation = spawnrotation;
        HealthAmount = 50f;

    }
}
