using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfCharge : MonoBehaviour
{
    [SerializeField] float waitTime = 1f;
    [SerializeField] float chargeTime = 1f;
    [SerializeField] float chargeSpeed = 1f;

    float waitTimer;
    float chargeTimer;
    Transform player;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        waitTimer = waitTime;
    }

    public void AttackMode()
    {
        if (waitTimer < 0)
        {
            transform.position += transform.up * chargeSpeed * Time.deltaTime;
            chargeTimer -= Time.deltaTime;
            if (chargeTimer < 0) waitTimer = waitTime;
        }
        else
        {
            RotateTowards(player.transform.position);
            waitTimer -= Time.deltaTime;
            if (waitTimer < 0) chargeTimer = chargeTime;
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
