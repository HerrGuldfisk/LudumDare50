using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireplace : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public Slider fuelSlider;
    public SpriteRenderer burnIndicator;

    [Header("Fire Data")]
    public float factor = 1;
    
    public float fuel = 0;

    public float maxFuel = 100f;

    public bool burning;

    public float burningSizeFactor = 7;


    void Start()
    {
        // anim.Play("Burning");
    }


    void Update()
    {
        // Don't update if no fuel.
        if(fuel <= 0) { return; }

        // Starting burn animation.
        if (!burning)
        {
            anim.Play("Burning");
            burnIndicator.enabled = true;
            burning = true;
        }

        // Burning
        fuel -= factor * Time.deltaTime;

        float indicatorSize = 3 + burningSizeFactor * (fuel / maxFuel);

        burnIndicator.transform.localScale = new Vector3(indicatorSize, indicatorSize, 1);
        // Removed
        // fuelSlider.value = fuel / maxFuel;

        // Checking fuel
        if(fuel <= 0)
        {
            fuel = 0;
            burning = false;
            burnIndicator.enabled = false;
            anim.Play("Idle");
        }

    }

    public void AddFireWood(float amount)
    {
        fuel += amount;
        Mathf.Clamp(fuel, 0, maxFuel);
    }
}
