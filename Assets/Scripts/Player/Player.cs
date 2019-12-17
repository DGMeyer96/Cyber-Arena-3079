using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float health;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public string saveName;
    public float playTime = 0.0f;
    public string playDate;
    public byte[] texData;
    public PlayerUI PlayerUI;

    public void NewGame()
    {
        health = 10;
        playTime = 0.0f;

        Debug.Log("[PLAYER] Creating new game: " + saveName);

        SaveSystem.NewPlayerData(this);
    }

    public void SaveGame()
    {
        playDate = DateTime.Now.ToString();
        playTime = Time.timeSinceLevelLoad;
        texData = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG();

        Debug.Log("[PLAYER] Play Date: " + playDate);
        Debug.Log("[PLAYER] Play Time: " + playTime);

        SaveSystem.SavePlayerData(this);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayerData(saveName);

        health = data.health;
        saveName = data.saveName;
        playTime = data.playTime;
        playDate = data.playDate;

        playerPosition.x = data.playerPosition[0];
        playerPosition.y = data.playerPosition[1];
        playerPosition.z = data.playerPosition[2];

        playerRotation.x = data.playerRotation[0];
        playerRotation.y = data.playerRotation[1];
        playerRotation.z = data.playerRotation[2];
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        //Debug.Log("Damage: " + health);
        PlayerUI.UpdateSlider(health);
    }
}
