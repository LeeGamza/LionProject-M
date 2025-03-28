using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public GameObject SpawnEnemy(GameObject prefab, Vector2 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }

    public Vector2 GetRandomSpawnPosition()
    {
        float camSize = mainCamera.orthographicSize;
        float camAspect = mainCamera.aspect;

        float randomX = Random.Range(-camAspect * camSize, camAspect * camSize);
        float randomY = mainCamera.transform.position.y + camSize + 1f;

        return new Vector2(randomX, randomY);
    }

    public Vector3 GetPlayerSpawnPosition()
    {
        return new Vector3(0f, -6f, 0f);
    }
    
    public GameObject SpawnPlayer()
    {
        Vector3 spawnPos = GetPlayerSpawnPosition();
        Quaternion rot = Quaternion.identity;

        GameObject player = PoolManager.Instance.Spawn(playerPrefab, spawnPos, rot);
        return player;
    }
}