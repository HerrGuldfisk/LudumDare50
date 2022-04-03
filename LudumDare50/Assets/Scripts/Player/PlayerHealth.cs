using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer outline;
    [SerializeField] Color dmgOutlineColor;
    [SerializeField] Color regOutlineColor;
    lifeBar bar;

    public float maxHp = 100.0f;
    public float currentHp;

    public float hpRate = 1.4f;
    public float hpDeg = 1.0f;
    public float hpDRest = -5f;
    public float hpDNormal = 1.2f;
    public float hpDRun = 3.5f;

    PlayerMovement pmScript;

    void Start()
    {
        pmScript = GetComponent<PlayerMovement>();
        bar = FindObjectOfType<lifeBar>();

        currentHp = maxHp;
        InvokeRepeating("PlayerDegen", 1f, 1f);

        TurnOffOutline();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("FireplaceDrop"))
        {
            if (col.gameObject.GetComponentInParent<Fireplace>().burning)
            {
                hpDeg = hpDRest;
                outline.color = regOutlineColor;
            }
            else
            {
                outline.color = dmgOutlineColor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FireplaceDrop"))
        {
            outline.color = dmgOutlineColor;
        }
    }

    void PlayerDegen()
    {
        if (currentHp <= maxHp)
        {
            currentHp -= hpDeg * hpRate;
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

        bar.SetLife(currentHp / maxHp);

        //outlineblink
        outline.enabled = true;
        Invoke("TurnOffOutline", 0.3f);
    }

    void TurnOffOutline()
    {
        outline.enabled = false;
    }
}
