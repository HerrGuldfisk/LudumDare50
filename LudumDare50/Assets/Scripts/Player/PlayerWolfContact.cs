using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWolfContact : MonoBehaviour
{
    [SerializeField] CanvasGroup deathScreen;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Wolf"))
        {
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
