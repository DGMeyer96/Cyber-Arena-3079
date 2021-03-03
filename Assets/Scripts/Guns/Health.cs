using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : Bolt.EntityBehaviour<IBensState>
{
    /*
    public int localHealth;
    public int randomSpawn;
    public GameObject temptest;
    void Start()
    {
        temptest = GameObject.Find("SpawnBitch");
        localHealth = 50;
    }
    public override void Attached()
    {
        if (entity.IsOwner)
        {
            state.Health = localHealth;
        }
        state.AddCallback("Health", HealthCallback);
    }
    private void HealthCallback()
    {
        localHealth = state.Health;
        if (localHealth <= 0)
        {
            randomSpawn = Random.Range(0, 7);
            Death();
        }
    }
    public void TakeDamage(int damageTaken)
    {
        //HealthAmount -= damageTaken;
        state.Health -= damageTaken;
        Debug.Log("Health is : " + localHealth);


    }
    //TODO I need to have it respawn rather than delete self
    void Death()
    {
        GameObject SpawnObject = GameObject.FindWithTag("Spawn");
        if (SpawnObject != null && entity.IsOwner)
        {
            //int i = 0;
            Transform[] SpawnPoints = new Transform[SpawnObject.transform.childCount];

            for (int i = 0; i < SpawnObject.transform.childCount; i++)
            {
                Debug.LogError("Spawn point: " + SpawnObject.transform.GetChild(i).transform);
                SpawnPoints[i] = SpawnObject.transform.GetChild(i).transform;
            }

            int spawn = Random.Range(0, 7);
            this.transform.position = SpawnPoints[spawn].position;
            this.transform.rotation = SpawnPoints[spawn].rotation;

            localHealth = 50;
            state.Health = localHealth;
        }
            
           // // gameObject.SetActive(false);
           // //Destroy(gameObject);
           //// Debug.Log("I SWEAR TO GOD THIS BETTER COME BACK RIGHT" + temptest.GetComponent<MP_Level_1_Spawn_Data>().spawnPositions[randomSpawn]);

           // Vector3 spawnposition;
           // Debug.Log("I SWEAR TO GOD THIS BETTER COME BACK RIGHT" + spawnposition);

           // Quaternion spawnrotation;
           // //BoltNetwork.Instantiate(BoltPrefabs.Player, spawnposition, spawnrotation);
           // gameObject.transform.position = spawnposition;
           // Debug.Log("Hello?");
           // Debug.Log("Should be correct" + gameObject.transform.position);
           // gameObject.transform.rotation = spawnrotation;
           // HealthAmount = 50f;
            
        }
*/
}
