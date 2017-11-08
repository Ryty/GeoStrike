using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int healthPoints;
    private Enemy masterScript;

    public void OnHit(Vector2 hitPoint, float hitForce = 1f, float hitDamage = 0f)
    {
        GetComponent<Rigidbody2D>().AddForceAtPosition((new Vector2(transform.position.x, transform.position.y) - hitPoint).normalized  * hitForce, hitPoint, ForceMode2D.Impulse);

        healthPoints--;

        if (healthPoints <= 0)
            masterScript.HandleDeath();
    }

    public void SetHP(int amount)
    {
        healthPoints = amount;
    }

    public void SetMaster(Enemy target)
    {
        masterScript = target;
    }
}
