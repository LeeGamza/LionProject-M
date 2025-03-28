using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject spaceShip;
    [SerializeField] private GameObject humanPlayer;

    private IPlayerState humanState;
    private FireManager fireManager;
    private PlayerStateMachine stateMachine;
    private PlayerMovement movement;
    private Animator rightAnimator;
    private Animator leftAnimator;
    
    [Header("Bullets")]
    [SerializeField] private GameObject[] bulletPrefabs;

    [Header("Muzzles")]
    [SerializeField] private Transform muzzleMiddle;
    [SerializeField] private Transform muzzleLeft;
    [SerializeField] private Transform muzzleRight;
    
    [Header("Effects")]
    [SerializeField] private GameObject[] effectPrefabs;
    
    [Header("Speed")]
    [SerializeField] private float movementSpeed = 5f;
    
    private bool isDead = false;
    private bool constrainToScreen = false;
    private bool isInvincible = false;
    private bool canRevive = false;
    private Tween invincibleTween;
    public bool IsInvincible => isInvincible;
    public bool IsDead => isDead;
    
    private void Awake()
    {
        movement = new PlayerMovement(movementSpeed, transform);
        fireManager = new FireManager();
        stateMachine = new PlayerStateMachine();
    }
    
    private void Start()
    {
        BulletFactory.SetBullet(bulletPrefabs);
        fireManager.SetEffectPrefabs(effectPrefabs);
        
        humanState = new HumanState(humanPlayer, fireManager, muzzleMiddle, this);
        
        var spaceshipState = new SpaceShipState(
            spaceShip,
            fireManager,
            muzzleMiddle,
            new[] { muzzleLeft, muzzleRight },
            this,
            humanState
        );

        stateMachine.ChangeState(spaceshipState);
    }

    private void OnEnable()
    {
        EventManager.Instance.OnAttack += HandleAttack;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnAttack -= HandleAttack;
    }

    private void Update()
    {
        if (!isDead)
        {
            Vector3 movementVector = movement.CalculateMovement();
            transform.Translate(movementVector, Space.World);
        }
        if (constrainToScreen)
        {
            transform.position = movement.CantEscapeScreen();
        }
        
        stateMachine.Update();
    }
    
    private void HandleAttack()
    {
        stateMachine.CurrentState.Attack();
    }
    
    public void TakeDamage()
    {
        if (isInvincible) return;
        
        if (stateMachine.CurrentState is IDamageable damageable)
        {
            damageable.TakeHit();
        }
    }

    public void SetState(IPlayerState state)
    {
        stateMachine.ChangeState(state);
    }

    public float GetInputValue()
    {
        return movement.Horizontal;
    }
    public void KillPlayer()
    {
        gameObject.SetActive(false);
    }
    
    public void SetDead()
    {
        isDead = true;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.pickup_machineGun);
            stateMachine.OnPickupItem();
            Destroy(other.gameObject);
        }
    }

    public void StartInvincibility(float duration = 1f)
    {
        isInvincible = true;
        invincibleTween?.Kill();

        invincibleTween = DOVirtual.DelayedCall(duration, () =>
        {
            isInvincible = false;
            Debug.Log("Not Invincible Mode");
        });
    }
    public bool CanRevive()
    {
        return isDead && !isInvincible;
    }

    public void Revive()
    {
        if (isDead)
        {
            gameObject.SetActive(true);
            GoPosition();

            if (TryGetComponent<Rigidbody2D>(out var rb))
            {
                rb.gravityScale = 0f;
            }
            StartInvincibility();
        }
        isDead = false;
    }

    public void GoPosition(float targetY = -3.2f, float duration = 1.2f)
    {
        constrainToScreen = false;
        transform.DOMoveY(targetY, duration).SetEase(Ease.OutCubic).
            OnComplete(() => 
            {
            constrainToScreen = true; 
            });
        constrainToScreen = true;
    }

    public FireManager GetFireManager()
    {
        return fireManager;
    }
}
