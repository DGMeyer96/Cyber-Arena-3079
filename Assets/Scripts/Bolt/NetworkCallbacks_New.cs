using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

[BoltGlobalBehaviour]
public class NetworkCallbacks_New : Bolt.GlobalEventListener
{
    public Vector3[] spawnPositions = new Vector3[8];
    public Quaternion[] spawnRotations = new Quaternion[8];

    public override void SceneLoadLocalDone(string scene)
    {
        /*
        GameObject tmp = GameObject.Find("SpawnPoints");
        Debug.Log("Found: " + tmp.name);
        for (int i = 0; i < tmp.transform.childCount; i++)
        {
            spawnPoints[i] = tmp.transform.GetChild(i).transform;
            Debug.Log("Spawn Point: " + tmp.transform.GetChild(i).transform);
        }
        */
        SetupSpawnPoints();

        //Transform spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];
        int spawn = Random.Range(0, 7);
        var spawnPosition = spawnPositions[spawn];
        var spawnRotation = spawnRotations[spawn];

        // instantiate cube
        BoltNetwork.Instantiate(BoltPrefabs.Player, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    void SetupSpawnPoints()
    {
        if(PlayerPrefs.GetString("LevelToLoad", "NULL") == "MP_Level_1")
        {
            spawnPositions[0] = new Vector3(10, 5, 10);
            spawnRotations[0] = new Quaternion(0, -135, 0, 0);

            spawnPositions[1] = new Vector3(10, 15, 70);
            spawnRotations[1] = new Quaternion(0, -135, 0, 0);

            spawnPositions[2] = new Vector3(-50, 5, 70);
            spawnRotations[2] = new Quaternion(0, -135, 0, 0);

            spawnPositions[3] = new Vector3(-70, 15, -10);
            spawnRotations[3] = new Quaternion(0, 45, 0, 0);

            spawnPositions[4] = new Vector3(-10, 5, -10);
            spawnRotations[4] = new Quaternion(0, 45, 0, 0);

            spawnPositions[5] = new Vector3(-10, 15, 50);
            spawnRotations[5] = new Quaternion(0, 45, 0, 0);

            spawnPositions[6] = new Vector3(-70, 5, 50);
            spawnRotations[6] = new Quaternion(0, 45, 0, 0);

            spawnPositions[7] = new Vector3(-50, 15, 10);
            spawnRotations[7] = new Quaternion(0, -135, 0, 0);
        }
        else if(PlayerPrefs.GetString("LevelToLoad", "NULL") == "MP_Level_2")
        {
            spawnPositions[0] = new Vector3(0, 5, 44);
            spawnRotations[0] = new Quaternion(0, 180, 0, 0);

            spawnPositions[1] = new Vector3(0, 5, -44);
            spawnRotations[1] = new Quaternion(0, 0, 0, 0);

            spawnPositions[2] = new Vector3(-22, 15, 0);
            spawnRotations[2] = new Quaternion(0, 90, 0, 0);

            spawnPositions[3] = new Vector3(-22, 15, 0);
            spawnRotations[3] = new Quaternion(0, -90, 0, 0);

            spawnPositions[4] = new Vector3(22, 5, 0);
            spawnRotations[4] = new Quaternion(0, -90, 0, 0);

            spawnPositions[5] = new Vector3(-22, 5, 0);
            spawnRotations[5] = new Quaternion(0, -90, 0, 0);

            spawnPositions[6] = new Vector3(0, 15, 40);
            spawnRotations[6] = new Quaternion(0, -180, 0, 0);

            spawnPositions[7] = new Vector3(0, 15, -40);
            spawnRotations[7] = new Quaternion(0, 0, 0, 0);
        }
    }
}
