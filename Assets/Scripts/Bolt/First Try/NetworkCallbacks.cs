﻿using UnityEngine;
using System.Collections;

[BoltGlobalBehaviour]
public class NetworkCallbacks : Bolt.GlobalEventListener
{
    public override void SceneLoadLocalDone(string map)
    {
        // randomize a position
        var spawnPosition = new Vector3(Random.Range(-8, 8), 7.1f, Random.Range(-8, 8));

        // instantiate cube
        BoltNetwork.Instantiate(BoltPrefabs.BoltPlayer, spawnPosition, Quaternion.identity);
    }
}