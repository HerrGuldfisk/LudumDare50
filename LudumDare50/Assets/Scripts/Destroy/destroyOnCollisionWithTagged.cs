using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyOnCollisionWithTagged : MonoBehaviour
{
    // DESTROYS THIS OBJECT WHEN IT COLLIDES WITH SOMETHING THAT HAS ONE OF THE STATED TAGS
    // REQUIRES A COLLIDER COMPONENT ON THIS ONE

    [SerializeField] string[] tags;
    [SerializeField] bool onTrigger = true;
    [SerializeField] bool onCollision = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!onTrigger) return;

        foreach(string tag in tags)
        {
            if (collision.CompareTag(tag))
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!onCollision) return;

        foreach (string tag in tags)
        {
            if (collision.gameObject.CompareTag(tag))
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!onTrigger) return;

        foreach(string tag in tags)
        {
            if (other.CompareTag(tag))
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {
        if (!onCollision) return;

        foreach (string tag in tags)
        {
            if (other.gameObject.CompareTag(tag))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
