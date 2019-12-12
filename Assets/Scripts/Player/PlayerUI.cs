using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Slider HealthSlider;

    // Start is called before the first frame update
    void Start()
    {
        HealthSlider = GameObject.Find("Health_Slider").GetComponent<Slider>();
        //Debug.Log("Initial Value: " + HealthSlider.value);
    }

    public void OnSliderValueChanged(float value)
    {
        HealthSlider.value = value;
    }

    public void UpdateSlider(float health)
    {
        HealthSlider.value = health;
        //Debug.Log("Health Slider: " + HealthSlider.value);
    }
}
