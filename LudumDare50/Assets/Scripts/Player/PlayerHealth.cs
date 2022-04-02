using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    lifeBar bar;

    public float maxHp = 100.0f;
    public float currentHp;

    public float hpDeg = 1.0f;
    public float hpDRest = -5f;
    public float hpDNormal = 1.2f;
    public float hpDRun = 3.5f;

    PlayerMovement pmScript;

    // Start is called before the first frame update
    void Start()
    {
        pmScript = GetComponent<PlayerMovement>();
        bar = FindObjectOfType<lifeBar>();

        currentHp = maxHp;
        InvokeRepeating("PlayerDegen", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D col)
    {
        hpDeg = hpDNormal;
        if (col.CompareTag("FireplaceDrop"))
        {
            if (col.gameObject.GetComponent<Fireplace>().burning)
            {
                hpDeg = hpDRest;
            }
        }
    }

    void PlayerDegen()
    {
        if (currentHp <= maxHp)
        {
            currentHp -= hpDeg * 1f;
        }

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        hpDeg = hpDNormal;

        if (currentHp < 0.1f)
        {
            GetComponent<PlayerWolfContact>().PlayerDeath();
        }

        bar.SetLife(currentHp/maxHp);
    }
}
