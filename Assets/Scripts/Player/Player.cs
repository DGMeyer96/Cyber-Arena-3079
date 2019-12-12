using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float health;
    public bool LeftVillage;
    public bool TrialOfStrength;
    public bool TrialOfMind;
    public bool TrialOfAgility;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public string saveName;
    public float playTime = 0.0f;
    public string playDate;
    //public Texture2D saveImage;
    public byte[] texData;
    public PlayerUI PlayerUI;

    private AudioSource deathSource;
    private AudioSource hitSource;
    private bool playdead;

    private void Start()
    {
        deathSource = GameObject.Find("PlayerDeath").GetComponent<AudioSource>();
        hitSource = GameObject.Find("PlayerHit").GetComponent<AudioSource>();
        playdead = true;
    }

    private void Update()
    {
        if (health <= 0 && playdead)
        {
            hitSource = null;
            deathSource.Play();
            GetComponent<Animator>().SetBool("Death", true);
            GetComponent<PlayerCharacterController>().enabled = false;
            GetComponent<KeyCombo>().enabled = false;
            playdead = false;
        }
    }

    public void NewGame()
    {
        health = 10;
        LeftVillage = false;
        TrialOfStrength = false;
        TrialOfMind = false;
        TrialOfAgility = false;
        playTime = 0.0f;

        Debug.Log("[PLAYER] Creating new game: " + saveName);

        SaveSystem.NewPlayerData(this);
    }

    public void SaveGame()
    {
        playDate = DateTime.Now.ToString();
        playTime = Time.timeSinceLevelLoad;
        //saveImage = ScreenCapture.CaptureScreenshotAsTexture();
        //texData = saveImage.EncodeToPNG();
        texData = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG();

        Debug.Log("[PLAYER] Play Date: " + playDate);
        Debug.Log("[PLAYER] Play Time: " + playTime);

        SaveSystem.SavePlayerData(this);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayerData(saveName);

        health = data.health;
        LeftVillage = data.LeftVillage;
        TrialOfStrength = data.TrialOfStrength;
        TrialOfMind = data.TrialOfMind;
        TrialOfAgility = data.TrialOfAgility;
        saveName = data.saveName;
        playTime = data.playTime;
        playDate = data.playDate;

        playerPosition.x = data.playerPosition[0];
        playerPosition.y = data.playerPosition[1];
        playerPosition.z = data.playerPosition[2];

        playerRotation.x = data.playerRotation[0];
        playerRotation.y = data.playerRotation[1];
        playerRotation.z = data.playerRotation[2];

        //saveImage.LoadImage(data.texData);
        //texData = data.texData;
    }

    public void DamagePlayer(int damage)
    {
        hitSource.Play();
        health -= damage;
        //Debug.Log("Damage: " + health);
        PlayerUI.UpdateSlider(health);
    }
}
