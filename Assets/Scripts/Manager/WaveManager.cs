using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<WaveData> waves;

    private int currentWaveIndex = 0;
    private List<GameObject> spawnedEnemies = new();
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>();
        StartNextWave();
    }

    void StartNextWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            Debug.Log("모든 웨이브 완료");
            return;
        }

        StartCoroutine(SpawnWave(waves[currentWaveIndex]));
        currentWaveIndex++;
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        spawnedEnemies.Clear();

        for (int i = 0; i < wave.enemyCount; i++)
        {
            Vector2 spawnPos = wave.useRandomSpawn ?
                spawnManager.GetRandomSpawnPosition() : wave.spawnPosition;

            GameObject enemy = spawnManager.SpawnEnemy(wave.enemyPrefab, spawnPos);
            spawnedEnemies.Add(enemy);

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        yield return StartCoroutine(WaitUntilAllEnemiesDead());
    }

    IEnumerator WaitUntilAllEnemiesDead()
    {
        while (true)
        {
            spawnedEnemies.RemoveAll(e => e == null);
            if (spawnedEnemies.Count == 0)
                break;

            yield return null;
        }

        StartNextWave();
    }
}
