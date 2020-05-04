using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UdpKit;
using Bolt.Matchmaking;

public class MultiplayerManager : Bolt.GlobalEventListener
{
    private string LevelToLoad;
    public void StartServer(string hostLevel)
    {
        LevelToLoad = hostLevel;
        PlayerPrefs.SetString("LevelToLoad", LevelToLoad);
        BoltLauncher.StartServer();
    }    
    public void StartClient()
    {
        BoltLauncher.StartClient();
    }
    public override void BoltStartDone()
    {
        if (BoltNetwork.IsServer)
        {
            string matchName = "Test Match";

            //BoltNetwork.SetServerInfo(matchName, null);
            BoltMatchmaking.CreateSession(matchName);

            BoltNetwork.LoadScene(LevelToLoad);
        }
    }

    public override void SessionListUpdated(Map<Guid, UdpSession> sessionList)
    {
        foreach (var session in sessionList)
        {
            UdpSession photonSession = session.Value as UdpSession;
            if (photonSession.Source == UdpSessionSource.Photon)
            {
                BoltNetwork.Connect(photonSession);
            }
        }
    }
}
