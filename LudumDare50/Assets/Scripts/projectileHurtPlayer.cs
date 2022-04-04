using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileHurtPlayer : MonoBehaviour
{
    [SerializeField] float projectileDmg = 15f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().LooseHP(projectileDmg);
            Destroy(gameObject);
        }
    }
}
