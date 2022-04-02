using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWolfContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Wolf"))
        {
            //Debug.Log("Collision with " + " " + col.name);
            PlayerDeath();
        }
    }

    private void PlayerDeath()
    {
        /*add death screen connection*/

        Destroy(this.gameObject);
    }
}
