using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Bolt;

public class PauseHandler : Bolt.GlobalEventListener
{
    public static bool gamePaused = false;
    private bool bDisconnect = false;

    public GameObject pauseMenuUI;
    public Player player;

    public Animator animator;

    private void Start()
    {
        //state.AddCallback("Disconnect", DisconnectClient);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("Pausing game");

            if (gamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
                //Debug.LogError("Disconnecting");
            }
        }

        if (gamePaused && Input.GetKeyDown(KeyCode.Y))
        {
            Debug.LogError("Disconnecting");
            //QuitGame();
            MainMenu();
        }
        
        else if (gamePaused && Input.GetKeyDown(KeyCode.N))
        {
            Resume();
        }

        if(bDisconnect)
        {
            Debug.Log("Disconnecting from game 2");
        }
    }

    public override void OnEvent(DisconnectFromServer evnt)
    {
        bDisconnect = true;
    }

    void DisconnectClient()
    {
        Debug.Log("Disconnecting from game 1");
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        EnableControl();
        gamePaused = false;
    }

    private void Pause()
    {
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        DisableControl();
        gamePaused = true;
    }

    public void MainMenu()
    {
        Debug.LogError("Loading: Main Menu");
        PlayerPrefs.SetInt("LevelToLoad", 0);
        //player.SaveGame();
        //BoltNetwork.Shutdown();
        animator.SetTrigger("FadeOut");
        //Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.LogError("Quitting Game");
        PlayerPrefs.SetInt("LevelToLoad", 0);
        BoltNetwork.Shutdown();
        Application.Quit();
    }

    public bool GetGamePaused()
    {
        return gamePaused;
    }

    private void DisableControl()
    {
        //player.GetComponent<BoltPlayerBehavior>().gamePaused = true;
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<BoltPlayerBehavior>().enabled = false;
        player.GetComponentInChildren<MouseLook>().enabled = false;
        player.GetComponentInChildren<weaponPosition>().enabled = false;
    }

    private void EnableControl()
    {
        //player.GetComponent<BoltPlayerBehavior>().gamePaused = false;
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<BoltPlayerBehavior>().enabled = true;
        player.GetComponentInChildren<MouseLook>().enabled = true;
        player.GetComponentInChildren<weaponPosition>().enabled = true;
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(1);
        BoltNetwork.Shutdown();
    }
}
