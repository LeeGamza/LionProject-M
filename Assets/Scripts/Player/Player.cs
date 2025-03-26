using System;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject spaceShip;
    //[SerializeField] private GameObject hPlayer;
    private FireManager fireManager;
    private PlayerStateMachine stateMachine;
    private PlayerMovement movement;
    
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
        
        var spaceshipState = new SpaceShipState(
            spaceShip,
            fireManager,
            muzzleMiddle,
            new[] { muzzleLeft, muzzleRight }
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
        Vector3 movementVector = movement.CalculateMovement();
        transform.Translate(movementVector, Space.World);
        
        transform.position = movement.CantEscapeScreen();
        
        stateMachine.Update();
    }
    
    private void HandleAttack()
    {
        stateMachine.CurrentState.Attack();
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
}
