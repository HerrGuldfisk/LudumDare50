using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHp = 100.0f;
    public float currentHp;

    float hpDeg = 1.0f;
    float hpDegM = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        InvokeRepeating("PlayerDegen", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Light"))
        {
            hpDegM = 0.1f;
        }
        else
        {
            hpDegM = 1f;
        }
    }

    void PlayerDegen()
    {
        if (currentHp < maxHp)
        {
            currentHp -= hpDegM * hpDeg;
        }

        if (currentHp < 0.1f)
        {
            GetComponent<PlayerWolfContact>().PlayerDeath();
        }

        Debug.Log("Drain HP");
    }
}
