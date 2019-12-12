using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickListener : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlayerPrefs.SetInt("LevelToLoad", 0);
            animator.SetTrigger("FadeOut");
            //gameObject.GetComponent<StartupLoader>().LoadLevel(0);
        }
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(1);
        //PlayerPrefs.SetInt("LevelToLoad", 0);
    }
}
