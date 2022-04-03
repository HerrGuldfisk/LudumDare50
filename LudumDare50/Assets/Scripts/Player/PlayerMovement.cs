using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 target;

    public float currentMoveSpeed;
    public float moveSpeed = 3f;
    public float runSpeed = 5f;
    public float dashSpeed = 18f;

    public float dashLen = 0.05f;
    public float dashCooldown = 1.0f;

    private float dashTimer;
    private float dashCooldownTimer;

    public float runTimer;

    private Vector2 moveDirection;

    private void Start()
    {
        currentMoveSpeed = moveSpeed;
    }

    void Update()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            target = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if(runTimer <= 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, currentMoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(moveDirection.magnitude != 0)
            {
                if(runTimer <= 0)
                {
                    transform.position += (Vector3)moveDirection * currentMoveSpeed * Time.deltaTime;
                }
            }
        }

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                currentMoveSpeed = moveSpeed;
                dashCooldownTimer = dashCooldown;
            }
        }

        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }
    }

    public void OnSpace(InputValue value)
    {
        if (value.Get<float>() == 1)
        {
            if (dashTimer <= 0 && dashCooldownTimer <= 0)
            {
                currentMoveSpeed = dashSpeed;
                GetComponent<PlayerHealth>().PlayerDashLoss();
                dashTimer = dashLen;
            }
        }
    }

    public void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
}
