using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    public int pointsYield;
    [Header("Enemy settings")]
    public int healthMax;

    private EnemyHealth healthScript;
    private GameMode currentGM;

    // Use this for initialization
    void Start ()
    {
        healthScript = GetComponent<EnemyHealth>();
        healthScript.SetHP(healthMax);
        healthScript.SetMaster(this);
	}

    private void Awake()
    {
        currentGM = FindObjectOfType<GameMode>();
    }

    public void HandleDeath()
    {
        handleGameMode();

        Destroy(gameObject);
    }

    private void handleGameMode()
    {
        if (currentGM is GameModeSurvival)
        {
            GameModeSurvival survivalMode = (GameModeSurvival)currentGM;

            survivalMode.HandleEnemyDeath(this);
        }
    }
}
