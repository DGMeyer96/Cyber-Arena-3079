using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject controlsMenu;
    public GameObject newGameMenu;
    public GameObject loadGameMenu;

    public Texture2D dataErrorTex;
    public Texture2D dataFoundTex;

    public Animator animator;

    private string saveGameName;

    private void Start()
    {
        Cursor.visible = true;
    }

    public void MainMenu(GameObject prevMenu)
    {
        mainMenu.gameObject.SetActive(true);
        prevMenu.gameObject.SetActive(false);
    }

    public void NewGameMenu()
    {
        //Debug.Log("[GAMEMANAGER] New Game Menu");

        //Active newGameMenu UI and deactivate mainMenu UI
        newGameMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);

        //Get refrences to each save game UI element (contains image and text description)
        GameObject saveGame1 = GameObject.Find("SaveGame1");
        //Debug.Log("[GAMEMANAGER] Found SaveGame1");
        GameObject saveGame2 = GameObject.Find("SaveGame2");
        //Debug.Log("[GAMEMANAGER] Found SaveGame2");
        GameObject saveGame3 = GameObject.Find("SaveGame3");
        //Debug.Log("[GAMEMANAGER] Found SaveGame3");

        //Get existing player data
        PlayerData save1 = SaveSystem.LoadPlayerData("Save1.sav");
        //Debug.Log("[GAMEMANAGER] " + save1.saveName);
        PlayerData save2 = SaveSystem.LoadPlayerData("Save2.sav");
        //Debug.Log("[GAMEMANAGER] " + save2.saveName);
        PlayerData save3 = SaveSystem.LoadPlayerData("Save3.sav");
        //Debug.Log("[GAMEMANAGER] " + save3.saveName);

        Texture2D tex = new Texture2D(2, 2);
        string time;

        //Check if Player Data exists
        if (save1 != null)
        {
            //Set image to saved image
            tex = new Texture2D(2, 2);
            tex.LoadImage(save1.texData);
            saveGame1.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            //saveGame1.transform.GetChild(0).GetComponent<RawImage>().texture = dataFoundTex;
            //Set description text to saved date and time
            time = (int)(save1.playTime / 60) + "min " + (int)(save1.playTime % 60) + "sec";
            saveGame1.transform.GetChild(3).GetComponent<Text>().text = "Play Time: " + time + "\n" + "Date: " + save1.playDate;
        }
        else
        {
            Debug.Log("[GAMEMANAGER] No save found");
            tex = new Texture2D(2, 2);
            //No existing Player Data, use defaults
            saveGame1.transform.GetChild(1).GetComponent<RawImage>().texture = dataErrorTex;
            saveGame1.transform.GetChild(3).GetComponent<Text>().text = "Play Time: N/A" + "\n" + "Date: N/A";
        }

        if (save2 != null)
        {
            tex = new Texture2D(2, 2);
            tex.LoadImage(save2.texData);
            //Set image to saved image
            saveGame2.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            //saveGame2.transform.GetChild(0).GetComponent<RawImage>().texture = dataFoundTex;
            //Set description text to saved date and time
            time = (int)(save2.playTime / 60) + "min " + (int)(save2.playTime % 60) + "sec";
            saveGame2.transform.GetChild(3).GetComponent<Text>().text = "Play Time: " + time + "\n" + "Date: " + save2.playDate;
        }
        else
        {
            tex = new Texture2D(2, 2);
            //No existing Player Data, use defaults
            saveGame2.transform.GetChild(1).GetComponent<RawImage>().texture = dataErrorTex;
            saveGame2.transform.GetChild(3).GetComponent<Text>().text = "Play Time: N/A" + "\n" + "Date: N/A";
        }

        if (save3 != null)
        {
            tex = new Texture2D(2, 2);
            tex.LoadImage(save3.texData);
            //Set image to saved image
            saveGame3.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            //saveGame3.transform.GetChild(0).GetComponent<RawImage>().texture = dataFoundTex;
            //Set description text to saved date and time
            time = (int)(save3.playTime / 60) + "min " + (int)(save3.playTime % 60) + "sec";
            saveGame3.transform.GetChild(3).GetComponent<Text>().text = "Play Time: " + time + "\n" + "Date: " + save3.playDate;
        }
        else
        {
            tex = new Texture2D(2, 2);
            //No existing Player Data, use defaults
            saveGame3.transform.GetChild(1).GetComponent<RawImage>().texture = dataErrorTex;
            saveGame3.transform.GetChild(3).GetComponent<Text>().text = "Play Time: N/A" + "\n" + "Date: N/A";
        }
    }

    public void LoadGameMenu()
    {
        loadGameMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);

        //Get refrences to each save game UI element (contains image and text description)
        GameObject loadGame1 = GameObject.Find("LoadGame1");
        GameObject loadGame2 = GameObject.Find("LoadGame2");
        GameObject loadGame3 = GameObject.Find("LoadGame3");

        //Get existing player data
        PlayerData load1 = SaveSystem.LoadPlayerData("Save1.sav");
        PlayerData load2 = SaveSystem.LoadPlayerData("Save2.sav");
        PlayerData load3 = SaveSystem.LoadPlayerData("Save3.sav");

        Texture2D tex = new Texture2D(2, 2);
        string time;

        //Check if Player Data exists
        if (load1 != null)
        {
            Debug.Log("Load 1 found");
            Debug.Log(loadGame1.ToString());
            //Set image to saved image

            tex = new Texture2D(2, 2);
            tex.LoadImage(load1.texData);
            loadGame1.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            //loadGame1.transform.GetChild(0).GetComponent<RawImage>().texture = dataFoundTex;
            //Set description text to saved date and time
            time = (int)(load1.playTime / 60) + "min " + (int)(load1.playTime % 60) + "sec";
            loadGame1.transform.GetChild(3).GetComponent<Text>().text = "Play Time: " + time + "\n" + "Date: " + load1.playDate;
        }
        else
        {
            tex = new Texture2D(2, 2);
            //No existing Player Data, use defaults
            loadGame1.transform.GetChild(1).GetComponent<RawImage>().texture = dataErrorTex;
            loadGame1.transform.GetChild(3).GetComponent<Text>().text = "Play Time: N/A" + "\n" + "Date: N/A";
        }

        if (load2 != null)
        {
            Debug.Log("Load 2 found");
            Debug.Log(loadGame2.ToString());

            tex = new Texture2D(2, 2);
            tex.LoadImage(load2.texData);
            //Set image to saved image
            loadGame2.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            //loadGame2.transform.GetChild(0).GetComponent<RawImage>().texture = dataFoundTex;
            //Set description text to saved date and time
            time = (int)(load2.playTime / 60) + "min " + (int)(load2.playTime % 60) + "sec";
            loadGame2.transform.GetChild(3).GetComponent<Text>().text = "Play Time: " + time + "\n" + "Date: " + load2.playDate;
        }
        else
        {
            tex = new Texture2D(2, 2);
            //No existing Player Data, use defaults
            loadGame2.transform.GetChild(1).GetComponent<RawImage>().texture = dataErrorTex;
            loadGame2.transform.GetChild(3).GetComponent<Text>().text = "Play Time: N/A" + "\n" + "Date: N/A";
        }

        if (load3 != null)
        {
            Debug.Log("Load 3 found");
            Debug.Log(loadGame3.ToString());

            tex = new Texture2D(2, 2);
            tex.LoadImage(load3.texData);
            //Set image to saved image
            loadGame3.transform.GetChild(1).GetComponent<RawImage>().texture = tex;
            //loadGame3.transform.GetChild(0).GetComponent<RawImage>().texture = dataFoundTex;
            //Set description text to saved date and time
            time = (int)(load3.playTime / 60) + "min " + (int)(load3.playTime % 60) + "sec";
            loadGame3.transform.GetChild(3).GetComponent<Text>().text = "Play Time: " + time + "\n" + "Date: " + load3.playDate;
        }
        else
        {
            tex = new Texture2D(2, 2);
            //No existing Player Data, use defaults
            loadGame3.transform.GetChild(1).GetComponent<RawImage>().texture = dataErrorTex;
            loadGame3.transform.GetChild(3).GetComponent<Text>().text = "Play Time: N/A" + "\n" + "Date: N/A";
        }
    }

    public void OptionsMenu()
    {
        optionsMenu.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }

    public void ControlsMenu()
    {
        controlsMenu.gameObject.SetActive(true);
        optionsMenu.gameObject.SetActive(false);
    }

    public void BackToOptions()
    {
        optionsMenu.gameObject.SetActive(true);
        controlsMenu.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void SelectSaveGame(string saveGame)
    {
        saveGameName = saveGame;
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("NewGame", 1);
        PlayerPrefs.SetString("SaveGameName", saveGameName);
        PlayerPrefs.SetInt("LevelToLoad", 2);
        animator.SetTrigger("FadeOut");
        //gameObject.GetComponent<StartupLoader>().LoadLevel(1);
        //SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        Debug.Log("Loading... " + saveGameName);
        PlayerPrefs.SetString("SaveGameName", saveGameName);
        PlayerPrefs.SetInt("LevelToLoad", 2);
        animator.SetTrigger("FadeOut");
        //gameObject.GetComponent<StartupLoader>().LoadLevel(1);
        //SceneManager.LoadScene(0);
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(1);
        //PlayerPrefs.SetInt("LevelToLoad", 2);
    }
}
