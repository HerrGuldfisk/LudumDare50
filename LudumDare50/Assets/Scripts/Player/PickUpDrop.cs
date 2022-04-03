using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Basics.Audio;

public class PickUpDrop : MonoBehaviour
{

    GameObject heldObject = null;
    List<GameObject> inRange = new List<GameObject>();
    GameObject canPickUp = null;

    public GameObject globalWoodContainer;

    PlayerMovement player;
    Vector2 heldObjectOffset = Vector2.zero;

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
                    if (canPickUp == obj) canPickUp = null;
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

        if (heldObject) heldObject.transform.position = transform.position;
    }

    public void OnLeftClick(InputValue value)
    {
        if (value.Get<float>() == 1)
        {
            if (heldObject != null)
            {
                GlobalMusicManager.Instance.PlayMusic("dropWood", false);
                DropObject();
                return;
            }

            TryPickupObject();
        }

        void DropObject()
        {
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
                GlobalMusicManager.Instance.PlayMusic("pickWood", false);
                heldObject = canPickUp;
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
