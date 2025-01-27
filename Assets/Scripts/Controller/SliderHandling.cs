using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHandling : MonoBehaviour
{
    public ShootingPlayer shooting;

    public bool runSlider = false;

    public bool readyToFire = true;

    public Slider fireballSlider;

    public float sliderSpeed;

    private void Update()
    {
        if (shooting.shootProjectile && !readyToFire)
        {
            runSlider = true;
            fireballSlider.value = 0;

            if (FindObjectOfType<CheatController>().noCooldown)            
                fireballSlider.value = 1;
            

            shooting.shootProjectile = false;
        }

        if (runSlider)
        {
            fireballSlider.value += sliderSpeed;

            shooting.shootProjectile = false;
        }

        if (fireballSlider.value >= 1)
        {
            readyToFire = true;
            runSlider = false;
        }
    }
}
