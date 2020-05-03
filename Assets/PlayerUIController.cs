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
        //healthSlider.value = health;
        healthSlider.value = Mathf.Lerp(health, healthSlider.value, 1 * Time.deltaTime);

    }
    public void setmaxShield(float Shield)//start with 0 shield
    {
        ShieldSlider.maxValue = Shield;
        ShieldSlider.value = 0;
    }

    public void SetShield(float Shield) 
    {
        //ShieldSlider.value = Shield;
        ShieldSlider.value = Mathf.Lerp(Shield, ShieldSlider.value, 1 * Time.deltaTime);
    }
}
