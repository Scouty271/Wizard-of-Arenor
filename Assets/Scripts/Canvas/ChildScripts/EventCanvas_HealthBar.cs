using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventCanvas_HealthBar : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        slider.maxValue = FindObjectOfType<Player>().maxHealth;
        slider.value = slider.maxValue;
    }


    public void updateSlider()
    {
        slider.value = FindObjectOfType<Player>().health;
    }
}
