using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutosaveHandler : MonoBehaviour
{
    public Player player;
    public Animator animator;

    public void AutosaveComplete()
    {
        Debug.Log("Autosave Complete");
        animator.SetBool("Saving", false);
    }
}
