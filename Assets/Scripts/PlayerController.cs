using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
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
        if (controlJoystick.inputDirection != Vector2.zero)
        {
            moveDir = controlJoystick.inputDirection * moveSpeed;

            float rot = Mathf.Rad2Deg * Mathf.Atan2(controlJoystick.inputDirection.y, controlJoystick.inputDirection.x) - 90f;

            transform.rotation = Quaternion.AngleAxis(rot, Vector3.forward);

            Debug.Log(rot);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDir);   
    }
}
