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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireplaceDrop"))
        {
            if(Vector3.Distance(collision.transform.position, transform.position) < 1.5f)
            {
                insideFire = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("FireplaceDrop"))
        {
            insideFire = false;
        }
    }

    public void OnDrop()
    {
        if (insideFire)
        {
            fireplace.AddFireWood(20);
            GetComponentInParent<WoodSpawnerManager>().currentLogs--;
            Destroy(gameObject);
        }
    }
}
