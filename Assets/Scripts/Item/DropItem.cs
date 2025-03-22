using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DropItem : MonoBehaviour
{
    [Header("아이템 드롭 설정")]
    public GameObject itemPrefab;
    public float dropChance = 100f;
    private Vector2 dropOffset = Vector2.zero;

    private void OnEnable()
    {
        EventManager.Instance.OnDrop += Drop;
    }
    
    private void OnDisable()
    {
        EventManager.Instance.OnDrop -= Drop;
    }

    public void Drop(Vector3 deathPosition)
    {
        if (itemPrefab == null) return;
        
        if (Random.Range(0f, 100f) <= dropChance)
        {
            Vector3 spawnPosition = deathPosition + new Vector3(dropOffset.x, dropOffset.y, 0);
            Instantiate(itemPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
