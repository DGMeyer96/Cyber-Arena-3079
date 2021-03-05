using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : Bolt.EntityBehaviour<IBensState>
{
    public float shield;
    public float maxhealth = 100;
    public float maxshield = 50;
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public string saveName;
    public float playTime = 0.0f;
    public string playDate;
    public byte[] texData;
    public BoltPlayerBehavior boltPlayerBehavior;
    //public PlayerUI PlayerUI;

    public PlayerUIController playerUIController;

    private AudioSource source;

    public int localHealth;
    public int randomSpawn;
    public GameObject temptest;
    void Start()
    {
        boltPlayerBehavior = GetComponent<BoltPlayerBehavior>();
        temptest = GameObject.Find("SpawnBitch");
        localHealth = 50;
        state.Health = localHealth;
        shield = 0;
        playerUIController.setmaxhealth(maxhealth);
        playerUIController.setmaxShield(maxshield);
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(25);
        }
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
        Debug.Log("Health is : " + localHealth);
        if (localHealth <= 0 && entity.IsOwner)
        {
            randomSpawn = UnityEngine.Random.Range(0, 7);
            Debug.Log("youdead");
            Death();
        }
    }
    public void TakeDamage(int damageTaken)
    {
        state.Health -= damageTaken;
    }
    public void HealPlayer(int addhealth)
    {
        state.Health += addhealth;
        playerUIController.SetHealth(localHealth);
    }

    public void shieldPlayer(int addshield)
    {
        //shield += addshield;
        //playerUIController.SetShield(shield);
    }

    //TODO I need to have it respawn rather than delete self
    void Death()
    {
        GameObject SpawnObject = GameObject.FindWithTag("Spawn");
        source.Play();
        if (SpawnObject != null && entity.IsOwner)
        {
            Transform[] SpawnPoints = new Transform[SpawnObject.transform.childCount];

            for (int i = 0; i < SpawnObject.transform.childCount; i++)
            {
                Debug.LogError("Spawn point: " + SpawnObject.transform.GetChild(i).transform);
                SpawnPoints[i] = SpawnObject.transform.GetChild(i).transform;
            }

            int spawn = UnityEngine.Random.Range(0, 7);
            this.transform.rotation = SpawnPoints[spawn].transform.rotation;

            boltPlayerBehavior.CharController.enabled = false;//cahracter controlelr doesnt like teleporting.  so turn it off when it dies

            state.SetTeleport(state.PlayerTransform);
            this.transform.position = SpawnPoints[spawn].transform.position;

            boltPlayerBehavior.CharController.enabled = true;

            localHealth = 50;
            state.Health = localHealth;
        }
    }

    //public void SaveGame()
    //{
    //    playDate = DateTime.Now.ToString();
    //    playTime = Time.timeSinceLevelLoad;
    //    texData = ScreenCapture.CaptureScreenshotAsTexture().EncodeToPNG();

    //    Debug.Log("[PLAYER] Play Date: " + playDate);
    //    Debug.Log("[PLAYER] Play Time: " + playTime);

    //    SaveSystem.SavePlayerData(this);
    //}

    //public void LoadGame()
    //{
    //    PlayerData data = SaveSystem.LoadPlayerData(saveName);

    //    localHealth = data.health;
    //    saveName = data.saveName;
    //    playTime = data.playTime;
    //    playDate = data.playDate;

    //    playerPosition.x = data.playerPosition[0];
    //    playerPosition.y = data.playerPosition[1];
    //    playerPosition.z = data.playerPosition[2];

    //    playerRotation.x = data.playerRotation[0];
    //    playerRotation.y = data.playerRotation[1];
    //    playerRotation.z = data.playerRotation[2];
    //}
}
