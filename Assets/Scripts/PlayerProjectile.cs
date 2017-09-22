using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float hitForce;
    public float hitDamage;

    private void Awake()
    {
        Debug.Log("Projectile spawned.");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(gameObject.name + " on collision with: " + col.gameObject.name);
        if (col.collider.gameObject.layer == 8) //8 = obstacle
        {
            OnContact();
        }
        else if(col.gameObject.GetComponent<EnemyHealth>())
        {
            col.gameObject.GetComponent<EnemyHealth>().OnHit(col.contacts[0].point, hitForce);
            OnContact();
        }
    }

    private void OnContact()
    {
        Destroy(gameObject);
    }
}
