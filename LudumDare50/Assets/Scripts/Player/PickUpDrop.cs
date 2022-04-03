using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpDrop : MonoBehaviour
{

    GameObject heldObject = null;
    List<GameObject> inRange = new List<GameObject>();
    GameObject canPickUp = null;

    PlayerMovement player;

    private void Start()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (inRange.Count > 0 && !heldObject)
        {
            foreach (GameObject obj in inRange)
            {
                if (!obj)
                {
                    inRange.Remove(obj);
                    if (canPickUp==obj) canPickUp = null;
                    return;
                }

                if (!canPickUp)
                {
                    canPickUp = obj;
                    canPickUp.GetComponent<SetOutline>().ToggleOutline(true);
                }
                else if (Vector3.Distance(obj.transform.position, transform.position) < Vector3.Distance(canPickUp.transform.position, transform.position))
                {
                    canPickUp = obj.transform.gameObject;
                    canPickUp.GetComponent<SetOutline>().ToggleOutline(true);
                }
                else if (!(canPickUp == obj))
                {
                    obj.GetComponent<SetOutline>().ToggleOutline(false);
                }
            }
        }

    }

    public void OnLeftClick(InputValue value)
    {
        if (value.Get<float>() == 1)
        {
            if (heldObject != null)
            {
                DropObject();
                return;
            }

            TryPickupObject();

            /*
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 1, Vector2.zero);
            GameObject closest = null;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Wood"))
                {
                    if (closest != null)
                    {
                        if (Vector3.Distance(hit.transform.position, transform.position) < Vector3.Distance(closest.transform.position, transform.position))
                        {
                            closest = hit.transform.gameObject;
                        }
                    }
                    else
                    {
                        closest = hit.transform.gameObject;
                    }
                }
            }

            if (closest != null)
            {
                heldObject = closest;
                heldObject.transform.parent = this.transform;
                heldObject.transform.position = transform.position;
                heldObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
            }
            */

        }

        void DropObject()
        {
            heldObject.transform.parent = null;

            // prepared for player animation
            // float dropDirection;
            // dropDirection = player.anim.FlipX ? -0.2f : 0.2f;
            //heldObject.transform.position += new Vector3(dropDirection, -0.25f, 0);

            heldObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            heldObject.GetComponent<WoodDrop>().OnDrop();
            heldObject.GetComponent<SetOutline>().ToggleOutline(true);
            if (!inRange.Contains(heldObject)) inRange.Add(heldObject);

            heldObject = null;
            return;
        }

        void TryPickupObject()
        {
            if (canPickUp)
            {
                heldObject = canPickUp;
                heldObject.transform.parent = this.transform;
                heldObject.transform.position = transform.position;
                heldObject.GetComponent<SpriteRenderer>().sortingOrder = 100;
                heldObject.GetComponent<SetOutline>().ToggleOutline(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wood" && !inRange.Contains(other.gameObject))
        {
            inRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Wood" && heldObject == null)
        {
            inRange.Remove(other.gameObject);
            other.GetComponent<SetOutline>().ToggleOutline(false);
            if (canPickUp == other.gameObject) canPickUp = null;
        }
    }

    private void LogList()
    {
        foreach (GameObject obj in inRange)
        {
            Debug.Log("Current list of objects in range: ");
            Debug.Log(obj.name);
        }
    }
}
