using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Basics.Audio;

public class Fireplace : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public Slider fuelSlider;
    public GameObject fireObject;
    public SpriteRenderer burnIndicator;

    [Header("Fire Data")]
    public float factor = 1;

    public float fuel = 0;

    public float maxFuel = 100f;

    public bool burning;

    private float nextDash = 0f;
    private float untilNextDash = 3f;

    public float burningSizeFactor = 7;
    PlayerHealth playerHealth;
    PlayerMovement playerMovement;

    void Start()
    {
        // anim.Play("Burning");
        playerHealth = FindObjectOfType<PlayerHealth>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }


    void Update()
    {
        // Don't update if no fuel.
        if (fuel <= 0) { return; }

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

        float fireSize = 0.3f + 0.7f * (fuel / maxFuel);
        fireObject.transform.localScale = new Vector3(fireSize, fireSize, 1);
        // Removed
        // fuelSlider.value = fuel / maxFuel;

        // Checking fuel
        if (fuel <= 0)
        {
            fuel = 0;
            burning = false;
            burnIndicator.enabled = false;
            anim.Play("Idle");
        }

        TrySetPlayerHP();
    }

    public void AddFireWood(float amount)
    {
        GlobalMusicManager.Instance.PlayMusic("fireBoost", false);

        if (fuel + amount <= maxFuel)
        {
            fuel += amount;
        }
        else
        {
            fuel = maxFuel;
        }

        TrySetPlayerHP();
        AddDash();
    }

    private void TrySetPlayerHP()
    {
        if (playerHealth) playerHealth.SetPlayerMaxHP((fuel/maxFuel) * 100);
    }

    private void AddDash()
    {
        nextDash += 1;
        Debug.Log(nextDash);
        if (nextDash >= untilNextDash)
        {
            Debug.Log(nextDash);
            nextDash = 0;
            playerMovement.maxDashCount += 1;
            playerMovement.bar.sizeDelta = new Vector2(playerMovement.maxDashCount * playerMovement.bar.sizeDelta.y / 2, playerMovement.bar.sizeDelta.y);
        }
    }
}
