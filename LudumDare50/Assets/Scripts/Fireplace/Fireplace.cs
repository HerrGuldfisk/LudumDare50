using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fireplace : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public Slider fuelSlider;

    [Header("Fire Data")]
    public float factor = 1;
    
    public float fuel = 0;

    public float maxFuel = 60f;

    public bool burning;


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
            burning = true;
        }

        // Burning
        fuel -= factor * Time.deltaTime;

        // Removed
        // fuelSlider.value = fuel / maxFuel;

        // Checking fuel
        if(fuel <= 0)
        {
            fuel = 0;
            burning = false;
            anim.Play("Idle");
        }

    }

    public void AddFireWood(float amount)
    {
        fuel += amount;
        Mathf.Clamp(fuel, 0, maxFuel);
    }
}
