using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    //  [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] bool destroy = true;
    [SerializeField] List<WaveConfig> bossWaves;
    [SerializeField] float pointsToSpawnBoss = 10000;
    int bossKilled = 0;
    




    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }

        while (looping);

    }


    void Update()
    {

        GameSession gameSession = FindObjectOfType<GameSession>();
        int scoreForBoss = gameSession.GetScoreForBoss();
        int numberOfBosses = GameObject.FindGameObjectsWithTag("Boss").Length;

        if (numberOfBosses == 0 && bossKilled == 1)
        {
            gameSession.SetScoreForBoss();
            bossKilled = 0;
            destroy = true;
            looping = true;
            StartCoroutine(Start());
        }


    }


    private IEnumerator SpawnAllWaves()
    {
        // Random waves
        var currentWave = waveConfigs[Random.Range(0, waveConfigs.Count)];
        yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        GameSession gameSession = FindObjectOfType<GameSession>();
        int scoreForBoss = gameSession.GetScoreForBoss();
        if (scoreForBoss >= pointsToSpawnBoss)
        {
            int bossWaveIndex = Random.Range(0, bossWaves.Count);
            var currentBossWave = bossWaves[bossWaveIndex];
            StopAllCoroutines();
            StartCoroutine(SpawnBossWave(currentBossWave));
        }



    }


    IEnumerator SpawnBossWave(WaveConfig waveConfig)
    {
        while (CheckIfEnemies()) yield return destroy = true;
        destroy = false;
        looping = false;
        var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
        newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);


    }



    public bool CheckIfEnemies()
    {
        int numberOfEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (numberOfEnemies != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void BossKilled()
    {
        bossKilled += 1;
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {

        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(
                waveConfig.GetEnemyPrefab(),
                waveConfig.GetWayPoints()[0].transform.position,
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }




    public bool GetDestroy() { return destroy; }
    public void SetDestroy(bool setDestroy) { destroy = setDestroy; }

}

    /*
      Random:
      var currentWave = waveConfigs[Random.Range(0, waveConfigs.Count)];
      yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

      Regular:
      for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
         var currentWave = waveConfigs[waveIndex];
         yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
        
    */


