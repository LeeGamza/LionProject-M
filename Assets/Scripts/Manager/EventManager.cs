using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public event Action<float, float> OnPlayerMove;
    public event Action OnUpMove;
    public event Action OnDownMove;
    public event Action OnAttack;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void InvokeUpMove()
    {
        OnUpMove?.Invoke();
    }
    public void InvokePlayerMove(float horizontal, float vertical)
    {
        OnPlayerMove?.Invoke(horizontal, vertical);
    }
    public void InvokeDownMove()
    {
        OnDownMove?.Invoke();
    }

    public void InvokeAttack()
    {
        OnAttack?.Invoke();
    }
}
