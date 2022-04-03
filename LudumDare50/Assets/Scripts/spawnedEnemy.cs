using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnedEnemy : MonoBehaviour
{
    private void Awake()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero, 0);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.tag == "Wolf") return;
            if (hit.transform.tag == "Wood") return;

            Debug.Log(gameObject.name + " was killed by spawning into collider: " + hit.collider.name);
            Destroy(gameObject);
            break;
        }
    }
}
