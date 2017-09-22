using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public void OnHit(Vector2 hitPoint, float hitForce = 1f, float hitDamage = 0f)
    {
        Debug.Log(gameObject.name + " just got hit.");
        GetComponent<Rigidbody2D>().AddForceAtPosition((hitPoint - new Vector2(transform.position.x, transform.position.y)).normalized  * hitForce, hitPoint, ForceMode2D.Impulse);
    }
}