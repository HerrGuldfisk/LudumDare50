using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpDrop : MonoBehaviour
{

    GameObject heldObject = null;

    public void OnLeftClick(InputValue value)
    {
        if (value.Get<float>() == 1)
        {
            if (heldObject != null)
            {
                heldObject.transform.parent = null;
                heldObject.GetComponent<WoodDrop>().OnDrop();
                heldObject = null;
                return;
            }

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
            }

        }
    }
}
