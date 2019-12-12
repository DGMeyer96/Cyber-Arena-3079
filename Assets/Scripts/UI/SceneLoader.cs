/* DEPRECIATED */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader: MonoBehaviour
{
    public int sceneIndex;
    public string saveGameName;
    public GameObject MainMenuUI;
    public GameObject CurrentUI;

    
    // Start is called before the first frame update
    /*
    void Start()
    {
        MainMenuUI = GameObject.Find("MainMenuUI");
        if(MainMenuUI != null)
        {
            Debug.Log("Found Main menu");
        }
        else
        {
            Debug.Log("Didn't find Main Menu UI");
        }
        NewGameUI = GameObject.Find("NewGameUI");
    }
    
    // Update is called once per frame
    void Update()
    {
        ChangeScene(sceneIndex);
    }
    */
    public void ChangeScene(int sceneNumber)
    {
        //Debug.Log("sceneBuildIndex to load: " + sceneNumber);
        switch(sceneNumber)
        {
            case 0:
                Debug.Log("Loading: Main Menu");
                SceneManager.LoadScene(sceneNumber);
                break;
            case 1:
                Debug.Log("Loading: Load Game");
                SceneManager.LoadScene(sceneNumber);
                break;
            case 2:
                Debug.Log("Loading: Options");
                SceneManager.LoadScene(sceneNumber);
                break;
            case 3:
                Debug.Log("Loading: New Game");
                /*
                MainMenuUI = GameObject.Find("MainMenuUI");
                CurrentUI = GameObject.Find("NewGameUI");
                MainMenuUI.SetActive(false);
                CurrentUI.SetActive(true);
                */
                SceneManager.LoadScene(sceneNumber);
                break;
            default:
                Debug.Log("Invalid scene number");
                break;
        }
        
    }

    public void MenuBack(string uiName)
    {
        CurrentUI = GameObject.Find(uiName);
        CurrentUI.SetActive(false);
        MainMenuUI = GameObject.Find("MainMenuUI");
        MainMenuUI.SetActive(true);
    }

    public void NewGame()
    {
        Debug.Log("New Game in Slot: " + saveGameName);
        PlayerPrefs.SetString("SaveGameName", saveGameName);
        PlayerPrefs.SetInt("NewGame", 1);
        SceneManager.LoadScene(4);
    }

    public void LoadGame()
    {
        Debug.Log("Loading... " + saveGameName);
        PlayerPrefs.SetString("SaveGameName", saveGameName);
        //Load game world then load in player data
        SceneManager.LoadScene(4);
    }

    public void SelectSaveGame(string saveGame)
    {
        saveGameName = saveGame;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
