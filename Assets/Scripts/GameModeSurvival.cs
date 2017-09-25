using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESurvivalState
{
    Unknown,
    Intermission,
    Wave,
    Over
};

public class GameModeSurvival : GameMode
{
    public ESurvivalState gameState;
    public int intermissionTime;
    public int waveCount = 1;
    public int enemyCount = 0;
    [HideInInspector]
    public int playerScore = 0, comboMeter = 1;

    private UIManager ui;
    private SpawnerSurvival[] spawners;

    public override IEnumerator StartGame()
    {
        ui = FindObjectOfType<UIManager>();
        spawners = FindObjectsOfType<SpawnerSurvival>();

        yield return StartCoroutine(HandleIntermission(10));

        while(gameState != ESurvivalState.Over)
        {
            yield return StartCoroutine(HandleWave());
            yield return StartCoroutine(HandleIntermission(intermissionTime));
        }

        Debug.Log("Completed!");

        yield return base.StartGame();
    }

    private IEnumerator HandleIntermission(int time)
    {
        gameState = ESurvivalState.Intermission;
        ui.timerUp.enabled = true;

        for(int i = time; i > 0; i--)
        {
            if(i > 5)
            {
                ui.timerUp.text = i.ToString();
            }
            else
            {
                ui.timerUp.enabled = false;
                ui.timerMid.enabled = true;
                ui.timerMid.text = i.ToString();
            }
            yield return new WaitForSeconds(1f);
        }

        ui.timerMid.enabled = false;
        yield break;
    }

    private IEnumerator HandleWave()
    {
        gameState = ESurvivalState.Wave;
        ui.scoreText.enabled = true;

        StartCoroutine(SpawnEnemies());

        Debug.Log("Spawning enemies finished, moving on with wave");
        while (enemyCount > 0)
        {
            yield return null;
        }

        ui.scoreText.enabled = false;
        yield break;
    }

    private IEnumerator SpawnEnemies()
    {
        foreach(SpawnerSurvival spawner in spawners)
        {
            spawner.Spawn(2);
        }

        Debug.Log("Spawned " + enemyCount + " enemies.");

        yield break;
    }
}
