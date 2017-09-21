using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("Projectile spawned.");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(gameObject.name + " on collision with: " + col.gameObject.name);
        if (col.collider.gameObject.layer == 8) //8 = obstacle
            Destroy(gameObject);
    }
}
