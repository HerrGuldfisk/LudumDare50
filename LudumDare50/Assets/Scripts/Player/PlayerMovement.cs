using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 target;

    public float moveSpeed = 3f;
    public float runSpeed = 5f;

    public float runTimeEachClick = 0.2f;
    private float runTimer;

    void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if(runTimer <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target, runSpeed * Time.deltaTime);
            }
        }

        runTimer -= Time.deltaTime;
    }

    public void OnSpace(InputValue value)
    {
        if(value.Get<float>() == 1)
        {
            runTimer = runTimeEachClick;
        }
    }
}
