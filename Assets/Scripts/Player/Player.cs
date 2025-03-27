using System;
using System.Collections;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject spaceShip;
    //[SerializeField] private GameObject hPlayer;
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
            new[] { muzzleLeft, muzzleRight },
            this
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
        UnEquipMachingun();
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

    public void EquipMachingun()
    {
        Transform right = spaceShip.transform.Find("SpaceShip_Right_0");
        Transform left = spaceShip.transform.Find("SpaceShip_Left_0");
        
        if (right != null && left != null)
        {
            rightAnimator = right.GetComponent<Animator>();
            leftAnimator = left.GetComponent<Animator>();

            StartCoroutine(PlayUpgradeAnimation());
        }
        else
        {
            Debug.LogWarning("SpaceShip_Right_0 또는 SpaceShip_Left_0 자식 오브젝트를 찾을 수 없습니다.");
        }
    }

    public void UnEquipMachingun()
    {
        int curruntammo = fireManager.Curruntammo();
        if (curruntammo <= 0)
        {
            StartCoroutine(BacktothoBasic());
        }
    }

    private IEnumerator PlayUpgradeAnimation()
    {
        rightAnimator.Play("SpaceShipMing_Right");
        leftAnimator.Play("SpaceShipMing_Left");
        yield return new WaitForSeconds(GetAnimationClipLength(rightAnimator, "SpaceShipMing_Right"));
        yield return new WaitForSeconds(GetAnimationClipLength(leftAnimator, "SpaceShipMing_Left"));
        rightAnimator.Play("SpaceShipM_Right");
        leftAnimator.Play("SpaceShipM_Left");
    }
    
    private float GetAnimationClipLength(Animator animator, string clipName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
            {
                return clip.length;
            }
        }
        Debug.LogWarning($"'{clipName}' 애니메이션 클립을 찾을 수 없습니다.");
        return 0f;
    }

    private IEnumerator BacktothoBasic()
    {
        rightAnimator.SetBool("0",true);
        leftAnimator.SetBool("0",true);
        yield break;
    }
}
