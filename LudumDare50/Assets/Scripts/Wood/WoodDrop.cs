using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDrop : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "FireplaceDrop")
        {
            collision.GetComponent<Fireplace>().AddFireWood(20);
            Destroy(this.gameObject);
        }
    }
}
