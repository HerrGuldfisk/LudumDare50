using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfChase : MonoBehaviour
{
    [SerializeField] float chaseSpeed = 2f;

    public void AttackMode(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.position += transform.up * chaseSpeed * Time.deltaTime;
    }
}
