using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float rotateSpeedModifier;
    public VirtualJoystick controlJoystick;

    private Vector2 moveDir;
    private Rigidbody2D rb;

    private void Start()
    {
        moveDir = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(controlJoystick.inputDirection != Vector2.zero)
        {
            HandleRotation();

            moveDir = transform.up * controlJoystick.inputDirection.magnitude * moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (controlJoystick.inputDirection != Vector2.zero)
            rb.AddForce(moveDir);
    }

    private void HandleRotation()
    {
        float rot = Mathf.Rad2Deg * Mathf.Atan2(controlJoystick.inputDirection.y, controlJoystick.inputDirection.x) - 90f;

        Quaternion startQuat = transform.rotation;
        Quaternion desiredQuat = Quaternion.Euler(0f, 0f, rot);

        transform.rotation = Quaternion.Slerp(startQuat, desiredQuat, rotateSpeedModifier * Time.deltaTime);

    }
}
