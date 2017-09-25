using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSurvival : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private BoxCollider2D boxComp;
    private GameModeSurvival gameMode;

    private void Start()
    {
        boxComp = GetComponent<BoxCollider2D>();
        gameMode = FindObjectOfType<GameModeSurvival>();
    }

    public void Spawn(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = new Vector3(MathLibrary.RandomPointInBounds(boxComp.bounds).x, MathLibrary.RandomPointInBounds(boxComp.bounds).y, 1);

            //GameObject go = 
            Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPos, new Quaternion());

            gameMode.enemyCount++;
        }
    }
}
