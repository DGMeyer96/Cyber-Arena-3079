using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float[] playerPosition;
    public float[] playerRotation;
    public bool LeftVillage;
    public bool TrialOfStrength;
    public bool TrialOfMind;
    public bool TrialOfAgility;
    public string saveName;
    public float playTime;
    public string playDate;
    //public Texture2D saveImage;
    public byte[] texData;


    public PlayerData(Player player)
    {
        health = player.health;

        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;

        playerRotation = new float[3];
        playerRotation[0] = player.transform.rotation.x;
        playerRotation[1] = player.transform.rotation.y;
        playerRotation[2] = player.transform.rotation.z;

        LeftVillage = player.LeftVillage;
        TrialOfStrength = player.TrialOfStrength;
        TrialOfMind = player.TrialOfMind;
        TrialOfAgility = player.TrialOfAgility;

        saveName = player.saveName;
        playTime = player.playTime;
        playDate = player.playDate;

        //texData = player.saveImage.EncodeToPNG();
        texData = player.texData;
        //saveImage = player.saveImage;
    }
}
