using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDrop : MonoBehaviour
{
    bool insideFire;

    Fireplace fireplace;

    private void Start()
    {
        fireplace = FindObjectOfType<Fireplace>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HeatZone"))
        {
            insideFire = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("HeatZone"))
        {
            insideFire = false;
        }
    }

    public void OnDrop()
    {
        if (insideFire)
        {
            fireplace.AddFireWood(20);
            Destroy(gameObject);
        }
    }
}
