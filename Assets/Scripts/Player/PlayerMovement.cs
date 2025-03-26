using UnityEngine;

public class PlayerMovement
{
    private readonly float movementSpeed;
    private readonly InputManager input;
    private readonly Transform playerTransform;
    private readonly Camera mainCamera;

    public PlayerMovement(float movementSpeed, Transform playerTransform)
    {
        this.movementSpeed = movementSpeed;
        this.playerTransform = playerTransform;
        input = InputManager.Instance;
        mainCamera = Camera.main;
    }

    public Vector3 CalculateMovement()
    {
        return new Vector3(input.horizontal, input.vertical, 0f) * movementSpeed * Time.deltaTime;
    }
    
    public Vector3 CantEscapeScreen()
    {
        Vector3 viewPos = mainCamera.WorldToViewportPoint(playerTransform.position);
        viewPos.x = Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        return mainCamera.ViewportToWorldPoint(viewPos);
    }
}
