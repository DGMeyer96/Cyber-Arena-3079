using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Slider healthSlider;
    public Slider ShieldSlider;

    public Text rifle;
    public Text sniper;
    public Text heavy;

    public void setmaxhealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health) 
    {
        healthSlider.value = Mathf.Lerp(health, healthSlider.value, 1 * Time.deltaTime);

    }
    public void setmaxShield(float Shield)
    {
        ShieldSlider.maxValue = Shield;
        ShieldSlider.value = 0;
    }

    public void SetShield(float Shield) 
    {
        ShieldSlider.value = Mathf.Lerp(Shield, ShieldSlider.value, 1 * Time.deltaTime);
    }

    public void SetAmmo(float pistolammo, float rifleammo, float sniperammo, float heavyammo)
    {
        rifle.text = "Rifle: " + rifleammo.ToString();
        sniper.text = "Sniper: " + sniperammo.ToString();
        heavy.text = "Heavy: " + heavyammo.ToString();
    }
}
