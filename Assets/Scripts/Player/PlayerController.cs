using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator upperAnimator;  
    public Animator lowerAnimator;
    private Rigidbody2D rb;
    
    public bool isMoving = false;
    public bool isJumping = false;
    private void Awake()
    {
        if (upperAnimator == null)
            upperAnimator = transform.Find("Upper")?.GetComponent<Animator>();
        if (lowerAnimator == null)
            lowerAnimator = transform.Find("Lower")?.GetComponent<Animator>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        isMoving = Mathf.Abs(InputManager.Instance.horizontal) > 0.01f;
        isJumping = InputManager.Instance.JumpPressed;

        if (isMoving)
        {
            EventManager.Instance?.InvokePlayerMove(InputManager.Instance.horizontal, 0f);
            flipPlayer();
        }
        PlayerAnimation();
    }

    private void flipPlayer()
    {
        if (InputManager.Instance.horizontal < 0f)
        {
            lowerAnimator.GetComponent<SpriteRenderer>().flipX = true;
            upperAnimator.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (InputManager.Instance.horizontal > 0f)
        {
            lowerAnimator.GetComponent<SpriteRenderer>().flipX = false;
            upperAnimator.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void PlayerAnimation()
    {
        upperAnimator?.SetBool("Move", isMoving);
        lowerAnimator?.SetBool("Move", isMoving);
        upperAnimator.SetBool("Jump", isJumping);
        lowerAnimator.SetBool("Jump", isJumping);
    }
}
