using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.SceneManagement;

[BoltGlobalBehaviour]
public class NetworkCallbacks_New : Bolt.GlobalEventListener
{
    public Vector3[] spawnPositions = new Vector3[8];
    public Quaternion[] spawnRotations = new Quaternion[8];

    public override void SceneLoadLocalDone(string scene)
    {
        SetupSpawnPoints();

        int spawn = Random.Range(0, 7);
        var spawnPosition = spawnPositions[spawn];
        var spawnRotation = spawnRotations[spawn];

        BoltNetwork.Instantiate(BoltPrefabs.Chad, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0));
    }

    public override void Disconnected(BoltConnection connection)
    {
        base.Disconnected(connection);
        Debug.LogError("Disconnected: Quitting");
        connection.Disconnect();
        //Application.Quit();
    }

    public override void BoltShutdownBegin(AddCallback registerDoneCallback)
    {
        Debug.LogError("BoltShutdownBegin: Quitting");
        Application.Quit();
        base.BoltShutdownBegin(registerDoneCallback);
    }

    public override void EntityDetached(BoltEntity entity)
    {
        Debug.LogError("EntityDetached: Quitting");
        //Application.Quit();
        //SceneManager.LoadScene(0);
        base.EntityDetached(entity);
        entity.Controller.Disconnect();
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
