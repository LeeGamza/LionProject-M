using UnityEngine;

[System.Serializable]
public class WaveData
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnInterval;
    public bool useRandomSpawn;
    public Vector2 spawnPosition;
}
