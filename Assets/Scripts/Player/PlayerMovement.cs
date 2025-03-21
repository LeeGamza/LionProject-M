using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float movementSpeed = 5f;
    private Rigidbody2D rb;
    private float jumpForce = 5f;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        EventManager.Instance.OnPlayerMove+= PlayerMove;
        EventManager.Instance.OnUpMove += UpMove;
        EventManager.Instance.OnDownMove += DownMove;
        EventManager.Instance.OnJump += JumpMove;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnPlayerMove -= PlayerMove;
        EventManager.Instance.OnUpMove -= UpMove;
        EventManager.Instance.OnDownMove -= DownMove;
        EventManager.Instance.OnJump -= JumpMove;
    }
     
    
    private void UpMove()
    {
        
    }
    
    private void DownMove()
    {
        
    }
    
    private void PlayerMove(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);
    }

    private void JumpMove()
    {
        if (rb == null)
        {
            Debug.Log("없어용");
        }
        if (Mathf.Abs(rb.linearVelocity.y) < 0.01f)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
    

