using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target = null;

    [SerializeField] bool isFollowing = true;

    [SerializeField] float lerpSpeed = 0.08f;

    void Update()
    {
        if (isFollowing && target != null)
        {
            Follow();
        }
    }

    public void Activate()
    {
        isFollowing = true;
    }

    public void Deactivate()
    {
        isFollowing = false;
    }

    private void Follow()
    {
        // If we later introduce distance based lerp.
        // float distance = DistanceToTarget();

        transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), lerpSpeed);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private float DistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.position);
    }
}
