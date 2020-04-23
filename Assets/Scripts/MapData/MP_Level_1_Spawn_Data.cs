using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_Level_1_Spawn_Data : MonoBehaviour
{
    public Vector3[] spawnPositions = new Vector3[8];
    public Quaternion[] spawnRotations = new Quaternion[8];

    // Start is called before the first frame update
    void Start()
    {
        SetupSpawnPoints();
        int spawn = Random.Range(0, 7);
        var spawnPosition = spawnPositions[spawn];
        var spawnRotation = spawnRotations[spawn];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetupSpawnPoints()
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

}
