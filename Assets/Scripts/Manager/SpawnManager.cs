using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveData
    {
        public float startTime;
        public GameObject enemyPrefab;
        public int enemyCount;
        public bool useRandomSpawn;
        public Vector2 spawnPosition;
        public float spawnInterval; 
    }

    public List<WaveData> waves = new List<WaveData>();
    private float elapsedTime = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        foreach (var wave in waves)
        {
            yield return new WaitForSeconds(wave.startTime - elapsedTime);
            StartCoroutine(SpawnWave(wave));
            elapsedTime = wave.startTime;
        }
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            Vector2 spawnPos = wave.useRandomSpawn ? GetRandomSpawnPosition() : wave.spawnPosition;
            GameObject enemyObj = Instantiate(wave.enemyPrefab, spawnPos, Quaternion.identity);

            yield return new WaitForSeconds(wave.spawnInterval);
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        float camSize = mainCamera.orthographicSize;
        float camAspect = mainCamera.aspect;

        float randomX = Random.Range(-camAspect * camSize, camAspect * camSize);
        float randomY = mainCamera.transform.position.y + camSize + 1f;

        return new Vector2(randomX, randomY);
    }
}