using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : MonoBehaviour
{
    [SerializeField] float viewDistance = 3.2f;
    [SerializeField] float timeBetweenThrows = 0.8f;
    [SerializeField] float projectileSpeed = 2f;
    [SerializeField] SpriteRenderer eyes;
    [SerializeField] GameObject projectile;
    Transform player;
    bool playerInRange = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating("TryAttack", timeBetweenThrows, timeBetweenThrows);
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Update()
    {
        if (player == null) return;

        if (Vector2.Distance(transform.position, player.position) < viewDistance)
        {
            eyes.color = Color.red;
            playerInRange = true;
            RotateTowards(player.position);
            return;
        }

        eyes.color = Color.white;
        playerInRange = false;
    }

    private void TryAttack()
    {
        if (player == null) return;
        if (!playerInRange) return;

        GameObject thrown = GameObject.Instantiate(projectile, transform.position, transform.rotation);
        thrown.GetComponent<Rigidbody2D>().velocity = transform.up * projectileSpeed;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewDistance);
        Gizmos.color = Color.red;
    }

}
