using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWolfContact : MonoBehaviour
{
    [SerializeField] CanvasGroup deathScreen;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wolf"))
        {
            //Debug.Log("Collision with " + " " + col.name);
            PlayerDeath();
        }
    }

    public void PlayerDeath()
    {
        deathScreen.alpha = 1;
        deathScreen.interactable = true;
        deathScreen.blocksRaycasts = true;

        Destroy(this.gameObject);
    }
}
