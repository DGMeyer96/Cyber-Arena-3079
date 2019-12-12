using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartupLoader : MonoBehaviour
{
    public Slider loadingBar;
    public Text progressText;
    public Animator animator;

    private AsyncOperation loadOperation;

    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            loadingBar = GameObject.Find("Loading_Slider").GetComponent<Slider>();
        }
        
        Time.timeScale = 1f;
    }

    public void LoadLevel(int sceneIndex)
    {
        //Debug.Log("Load Level");
        StartCoroutine(LoadLevelAsynchronously(sceneIndex));
    }

    IEnumerator LoadLevelAsynchronously(int sceneIndex)
    {
        loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!loadOperation.isDone)
        {
            //Scene loading has 2 phases.  The first phase is 0 -> 0.9 so we need to clamp the value from 0 -> 1
            //This will ensure the loading bar reflects 0 -> 100% accurately
            //Last 0.1 is simply switching the scene and removing old one (fast and doesn't need to be shown). 
            float loadProgress = Mathf.Clamp01(loadOperation.progress / .9f);

            loadingBar.value = loadProgress;
            progressText.text = ((int)loadProgress * 100f) + "%";

            //Debug.Log("Loading: " + progressText.text);

            
            if(loadProgress == 1.0f)
            {
                //Debug.Log("Begin Fade Out");
                //Debug.Log("Loading... " + sceneIndex);
                LevelFadeOut();
            }
            
            loadOperation.allowSceneActivation = false;
            //yield return loadOperation;

            yield return null;
        }
    }

    public void LevelFadeIn()
    {
        //Debug.Log("Level Faded In");
        animator.SetTrigger("FadeIn");
    }

    public void LevelFadeOut()
    {
        //Debug.Log("Level Faded Out");
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeInComplete()
    {
        //Debug.Log("Fade In complete... Loading Level: " + PlayerPrefs.GetInt("LevelToLoad"));
        LoadLevel(PlayerPrefs.GetInt("LevelToLoad"));
    }

    public void OnFadeOutComplete()
    {
        //Debug.Log("Fade Out complete... Activating Scene");
        ActivateScene();
    }

    public void ActivateScene()
    {
        loadOperation.allowSceneActivation = true;
    }
}
