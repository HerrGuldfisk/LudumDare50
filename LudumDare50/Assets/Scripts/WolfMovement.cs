using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    [SerializeField] float chaseSpeed = 2;
    [SerializeField] float patrolSpeed = 1;
    [SerializeField] float escapeFireSpeed = 1;
    [SerializeField] float viewDistance = 3;
    [SerializeField] float timeBetweenDirChange = 1f;
    [SerializeField] float minDistanceToFire = 3;
    [SerializeField] float maxDistanceToFire = 6;

    Transform player;
    Transform fire;
    float patrolTimer = 0;
    bool escapeFire = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fire = GameObject.FindGameObjectWithTag("Fire").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToFire = Vector2.Distance(transform.position, fire.position);

        if (distanceToFire < minDistanceToFire && !escapeFire)
        {
            escapeFire = true;
            RotateTowards(transform.position + (transform.position - fire.position));
        }
        else if (escapeFire)
        {
            MoveForward(escapeFireSpeed);

            if (distanceToFire > maxDistanceToFire) escapeFire = false;
        }
        else if (Vector2.Distance(transform.position, player.position) < viewDistance)
        {
            RotateTowards(player.position);
            MoveForward(chaseSpeed);
        }
        else
        {
            patrolTimer -= Time.deltaTime;

            if (patrolTimer < 0)
            {
                patrolTimer = timeBetweenDirChange;
                transform.Rotate(Vector3.forward * Random.Range(0, 360));
            }

            MoveForward(patrolSpeed);
        }

        void MoveForward(float speed)
        {
            transform.position += transform.up * speed * Time.deltaTime;
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, minDistanceToFire);
        Gizmos.DrawWireSphere(transform.position, maxDistanceToFire);
    }
}
