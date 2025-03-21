using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public bool JumpPressed { get; private set; }
    public float horizontal;

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
        if (Input.GetKeyDown(KeyCode.W)) EventManager.Instance?.InvokeUpMove();
        if (Input.GetKeyDown(KeyCode.S)) EventManager.Instance?.InvokeDownMove();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpPressed = true;
            EventManager.Instance?.InvokeJump();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpPressed = false;
        }
    }
}