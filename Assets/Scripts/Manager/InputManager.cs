using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public bool JumpPressed { get; private set; }
    public float horizontal;
    public float vertical;

    void Awake()
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

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        EventManager.Instance?.InvokePlayerMove(horizontal, vertical);
        if (Input.GetKeyDown(KeyCode.W)) EventManager.Instance?.InvokeUpMove();
        if (Input.GetKeyDown(KeyCode.S)) EventManager.Instance?.InvokeDownMove();
        if (Input.GetKeyDown(KeyCode.Space)) EventManager.Instance?.InvokeAttack();
    }
}