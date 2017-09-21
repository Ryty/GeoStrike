using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Player control settings")]
    public float moveSpeed;
    public float rotateSpeedModifier;
    public VirtualJoystick controlJoystick;
    [Header("Shooting settings")]
    public float shootDelay;
    public float projectileShootForce;
    public GameObject projectilePrefab;

    private Vector2 moveDir;
    private Rigidbody2D rb;
    private Timer shootTimer;

    private void Start()
    {
        moveDir = Vector2.zero;
        rb = GetComponent<Rigidbody2D>();

        shootTimer = gameObject.AddComponent<Timer>();
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

    public void OnShootButton()
    {
        Debug.Log("Shoot button clicked.");
        if (shootTimer.bIsFinished)
            Shoot();
    }

    private void Shoot()
    {
        GameObject projectile = GameObject.Instantiate<GameObject>(projectilePrefab, transform.position + transform.up, this.transform.rotation);
        projectile.GetComponent<Rigidbody2D>().AddForce(projectile.transform.up * projectileShootForce, ForceMode2D.Impulse);

        shootTimer.SetTimer(shootDelay);
    }
}
