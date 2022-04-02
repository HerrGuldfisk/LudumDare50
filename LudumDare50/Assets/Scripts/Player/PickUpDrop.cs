using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpDrop : MonoBehaviour
{

    GameObject heldObject = null;

    public void OnLeftClick(InputValue value)
    {
        if(value.Get<float>() == 1)
        {
            if(heldObject != null)
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Collider2D>().enabled = true;
                heldObject = null;
                return;
            }

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1, Vector2.zero);

            if(hit.collider.gameObject.CompareTag("Wood"))
            {
                heldObject = hit.collider.gameObject;
                heldObject.GetComponent<Collider2D>().enabled = false;
                heldObject.transform.parent = this.transform;
            }
        }
    }
}