using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
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
}