using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] SpriteRenderer outline;
    [SerializeField] Color dmgOutlineColor;
    [SerializeField] Color regOutlineColor;
    lifeBar barFill;
    RectTransform bar;
    [SerializeField] float barMaxWidth = 300f;

    public float maxHp = 100.0f;
    public float currentHp;

    public float hpRate = 2f;
    public float hpDeg = 1.0f;
    public float hpDRest = -5f;
    public float hpDNormal = 1.2f;
    public float hpDashLoss = 5f;

    bool inHeat = false;

    PlayerMovement pmScript;

    void Start()
    {
        pmScript = GetComponent<PlayerMovement>();
        barFill = FindObjectOfType<lifeBar>();
        bar = GameObject.FindGameObjectWithTag("LifeMeter").GetComponent<RectTransform>();

        currentHp = maxHp;
        InvokeRepeating("PlayerDegen", 1f, 1f);

        TurnOffOutline();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("HeatZone"))
        {
            if (col.gameObject.GetComponentInParent<Fireplace>().burning)
            {
                inHeat = true;
                outline.color = regOutlineColor;
            }
            else
            {
                inHeat = false;
                outline.color = dmgOutlineColor;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HeatZone"))
        {
            inHeat = false;
            outline.color = dmgOutlineColor;
        }
    }

    void PlayerDegen()
    {
        hpDeg = hpDNormal;
        CheckPlayerState();

        if (currentHp <= maxHp)
        {
            currentHp -= hpDeg * hpRate;
        }

        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }

        if (currentHp < 0.1f)
        {
            GetComponent<PlayerWolfContact>().PlayerDeath();
        }

        barFill.SetLife(currentHp / maxHp);

        //outlineblink
        outline.enabled = true;
        Invoke("TurnOffOutline", 0.3f);
    }

    void CheckPlayerState()
    {
        if (inHeat)
        {
            hpDeg = hpDRest;
        }
    }

    public void PlayerDashLoss()
    {
        currentHp -= hpDashLoss;

        if (currentHp < 0.1f)
        {
            GetComponent<PlayerWolfContact>().PlayerDeath();
        }

        barFill.SetLife(currentHp / maxHp);

        //outlineblink
        outline.enabled = true;
        Invoke("TurnOffOutline", 0.3f);
    }

    void TurnOffOutline()
    {
        outline.enabled = false;
    }

    public void SetPlayerMaxHP(float value)
    {
        float currentHealthPerc = currentHp/maxHp;
        maxHp = Mathf.Clamp(value, 20, 100);
        currentHp = maxHp*currentHealthPerc;
        bar.sizeDelta = new Vector2((maxHp/100) * barMaxWidth, bar.sizeDelta.y);
    }
}
