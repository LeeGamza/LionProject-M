using System;
using UnityEngine;

public class SpaceShipMoveController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float movementSpeed = 5.0f;
    private void OnEnable()
    {
        EventManager.Instance.OnPlayerMove+= PlayerMove;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnPlayerMove -= PlayerMove;
    }
    
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void PlayerMove(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, vertical, 0f);
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }
    
    
}
