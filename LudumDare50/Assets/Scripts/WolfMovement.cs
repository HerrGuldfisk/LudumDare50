using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basics.Audio;

public class WolfMovement : MonoBehaviour
{
    [SerializeField] float patrolSpeed = 0.8f;
    [SerializeField] float escapeFireSpeed = 1f;
    [SerializeField] float viewDistance = 3.2f;
    [SerializeField] float timeBetweenDirChange = 0.8f;
    [SerializeField] float fireEscapeTime = 2f;
    [SerializeField] SpriteRenderer eyes;

    Transform player;
    Transform fire;
    float patrolTimer = 0;
    bool escapeFire = false;
    bool inFireRange = false;
    float fireEscapeTimer = 0;

    WolfCharge wolfCharge;
    WolfChase wolfChase;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        fire = GameObject.FindGameObjectWithTag("Fire").transform;

        TryGetComponent<WolfCharge>(out wolfCharge);
        TryGetComponent<WolfChase>(out wolfChase);

        GlobalMusicManager.Instance.PlayMusic("wolfHowlGroup", false);
    }

    private void Update()
    {
        if (player == null) return;


        if (inFireRange && !escapeFire)
        {
            eyes.color = Color.white;
            escapeFire = true;
            fireEscapeTimer = fireEscapeTime;
            RotateTowards(transform.position + (transform.position - fire.position));
        }
        else if (escapeFire)
        {
            eyes.color = Color.white;
            MoveForward(escapeFireSpeed);
            fireEscapeTimer -= Time.deltaTime;
            if (fireEscapeTimer < 0) escapeFire = false;
        }
        else if (Vector2.Distance(transform.position, player.position) < viewDistance)
        {
            GlobalMusicManager.Instance.PlayMusic("wolfBark", false);
            eyes.color = Color.red;
            if (wolfCharge)
            {
                wolfCharge.AttackMode();
            }
            else if (wolfChase)
            {
                wolfChase.AttackMode(player.position);
            }
        }
        else
        {
            eyes.color = Color.white;
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
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "HeatZone")
        {
            if (other.GetComponentInParent<Fireplace>().burning)
            {
                inFireRange = true;
            }
            else
            {
                inFireRange = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "HeatZone")
        {
            inFireRange = false;
        }
    }
}
