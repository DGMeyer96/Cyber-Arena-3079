using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider ShieldSlider;

    public void setmaxhealth(float health)//start with max health
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health) 
    {
        healthSlider.value = health;
    }    
    public void setmaxShield(float Shield)//start with 0 shield
    {
        healthSlider.maxValue = Shield;
    }

    public void SetShield(float Shield) 
    {
        healthSlider.value = Shield;
    }
}
