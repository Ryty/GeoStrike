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
    [Header("Game mode settings")]
    public int intermissionTime;
    public float comboPerKill;
    public float comboTimerMax;
    [Header("Current values concering game mode")]
    public int waveCount;
    public int enemyCount { get; private set; }
    public int playerScore { get; private set; }
    [HideInInspector]
    public float comboMeter { get; private set; }

    private UIManager ui;
    private List<SpawnerSurvival> spawners = new List<SpawnerSurvival>();
    private float comboTimerCurrent;
    private Timer comboTimer;

    private void Start()
    {
        
    }

    //------------Handles starting the game and changing it depending on its' state---------
    public override IEnumerator StartGame()
    {
        SetStartingValues();
        comboTimer = gameObject.AddComponent<Timer>();

        ui = FindObjectOfType<UIManager>();        
        ui.SetComboTimer(comboTimer);
        
        foreach (SpawnerSurvival s in FindObjectsOfType<SpawnerSurvival>())
            spawners.Add(s);

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
        waveCount++;
        yield break;
    }

    private IEnumerator SpawnEnemies()
    {
        //Przygotowywanie listy spawnerów
        List<SpawnerSurvival> currentSpawners = new List<SpawnerSurvival>();
        foreach (SpawnerSurvival s in spawners) currentSpawners.Add(s);
        currentSpawners.Remove(FindNearestSpawner());

        foreach(SpawnerSurvival s in currentSpawners)
        {
            s.Spawn(CalculateEnemyCount(waveCount) / currentSpawners.Count);
        }

        Debug.Log(currentSpawners.Count + " Spawned " + enemyCount + " enemies.");

        yield break;
    }

    private void SetStartingValues()
    {
        waveCount = 1;
        enemyCount = 0;
        playerScore = 0;
        comboMeter = 1f;
    }
    //------------Handles spawning enemies---------
    private int CalculateEnemyCount(int wave)
    {
        if (wave == 1)
            return 8;
        else
            return CalculateEnemyCount(wave - 1) + (wave * CalculateEnemyCount(1) / 3);
    }

    private SpawnerSurvival FindNearestSpawner()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);

        SpawnerSurvival nearest = spawners[0];

        for (int i = 1; i < spawners.Count; i++)
            if (Vector2.Distance(playerPos, new Vector2(spawners[i].transform.position.x, spawners[i].transform.position.y)) < Vector2.Distance(playerPos, new Vector2(nearest.transform.position.x, nearest.transform.position.y)))
                nearest = spawners[i];

        return nearest;
    }

    public void HandleSpawningEnemy()
    {
        enemyCount++;
    }
    //------------Handles counting point and keeping track of number of enemies and combo meter---------
    public void HandleEnemyDeath(Enemy e)
    {
        enemyCount--;
        Debug.Log("Enemy destroyed. Enemies remaining: " + enemyCount);

        AddPoints(e.pointsYield);
        ComboUp();
    }

    private void AddPoints(int basePoints)
    {
        playerScore += (int)(basePoints * comboMeter);

        ui.SetScoreText(playerScore);
    }

    private void ComboUp()
    {
        if (!(comboMeter > 1f))
        {
            ui.comboText.enabled = true;
            ui.timerCombo.enabled = true;
        }

        comboMeter += comboPerKill;
        ui.SetComboText(comboMeter);

        comboTimer.SetTimer(comboTimerMax, false, ComboReset, true);
    }

    private void ComboReset()
    {
        Debug.Log("ComboReset called.");
        comboMeter = 1f;
        ui.comboText.enabled = false;
        ui.timerCombo.enabled = false;
    }
}
