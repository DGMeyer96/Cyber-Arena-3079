using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float health;
    public float shield;
    public float maxhealth = 100;
    public float maxshield = 50;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public string saveName;
    public float playTime = 0.0f;
    public string playDate;
    public byte[] texData;
    //public PlayerUI PlayerUI;

    public PlayerUIController playerUIController;

    public void Start()
    {
        health = 100;
        shield = 0;
        playerUIController.setmaxhealth(maxhealth);
        playerUIController.setmaxShield(maxshield);
    }
    public void NewGame()
    {
        health = 100;
        shield = 0;
        playerUIController.setmaxhealth(maxhealth);
        playerUIController.setmaxShield(maxshield);
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

    public void DamagePlayer(float damage)
    {
        if (shield == 0)
        {
            health -= damage;
            playerUIController.SetHealth(health);
        }
        else if (shield != 0)
        {
            if (damage > shield)
            {
                damage -= shield;
                shield = 0;
                playerUIController.SetShield(shield);
                DamagePlayer(damage);
            }
            else 
            {
                shield -= damage;
                playerUIController.SetShield(shield);
            }
        }
    }

    public void HealPlayer(int addhealth)
    {
        health += addhealth;
        playerUIController.SetHealth(health);
    }

    public void shieldPlayer(int addshield)
    {
        shield += addshield;
        playerUIController.SetShield(shield);
    }
}
