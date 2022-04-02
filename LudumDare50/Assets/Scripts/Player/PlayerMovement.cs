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
    public float runTimer;

    private Vector2 moveDirection;

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
        else
        {
            if(moveDirection.magnitude != 0)
            {
                if(runTimer <= 0)
                {
                    transform.position += (Vector3)moveDirection * moveSpeed * Time.deltaTime;
                }
                else
                {
                    transform.position += (Vector3)moveDirection * runSpeed * Time.deltaTime;
                }
            }
        }

        if(runTimer > 0)
        {
            runTimer -= Time.deltaTime;
        }
    }

    public void OnSpace(InputValue value)
    {
        if(value.Get<float>() == 1)
        {
            runTimer = runTimeEachClick;
        }
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
}
