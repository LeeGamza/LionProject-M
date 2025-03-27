using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private readonly Dictionary<string, Queue<GameObject>> _poolDict = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        string key = prefab.name;

        if (!_poolDict.ContainsKey(key))
            _poolDict[key] = new Queue<GameObject>();

        GameObject obj;

        if (_poolDict[key].Count > 0)
        {
            obj = _poolDict[key].Dequeue();
            obj.transform.SetPositionAndRotation(pos, rotation);
            obj.SetActive(true);
        }
        else
        {
            obj = Instantiate(prefab, pos, rotation);
            obj.name = key;
        }

        return obj;
    }
    
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        var key = obj.name.Replace("(Clone)", "").Trim();

        if (!_poolDict.ContainsKey(key))
            _poolDict[key] = new Queue<GameObject>();

        _poolDict[key].Enqueue(obj);
    }
}
