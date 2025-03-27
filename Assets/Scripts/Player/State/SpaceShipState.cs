using System.Collections;
using UnityEngine;
using Bullet;

public class SpaceShipState : IPlayerState, IPlayerPickupReceiver, IDamageable
{
    private int hp = 3;
    
    private SpriteRenderer rightRenderer;
    private SpriteRenderer leftRenderer;
    private readonly Color hitColor = Color.red;
    
    private readonly GameObject spaceShip;
    private readonly FireManager fireManager;
    private readonly Player player;
    
    private readonly Transform muzzleMiddle;
    private readonly Transform[] muzzleLeftRight;
    
    private readonly GameObject boosterL;
    private readonly GameObject boosterR;
    
    private Animator rightAnimator;
    private Animator leftAnimator;

    private bool isMachingunEquip = false;
    private bool isReturnToBasic = false;

    public SpaceShipState(GameObject spaceShip, 
        FireManager fireManager, 
        Transform muzzleMiddle, 
        Transform[] muzzleLeftRight,
        Player player)
    {
        this.spaceShip = spaceShip;
        this.fireManager = fireManager;
    
        this.muzzleMiddle = muzzleMiddle;
        this.muzzleLeftRight = muzzleLeftRight;
        this.player = player;
        
        boosterL = spaceShip.transform.Find("Booster_L")?.gameObject;
        boosterR = spaceShip.transform.Find("Booster_R")?.gameObject;
    }
    
    public void Enter()
    {
        spaceShip.SetActive(true);
        fireManager.SetBulletType(BulletType.Basic);
        fireManager.SetMuzzle(muzzleMiddle);
        
        GetComponents();
    }

    public void Attack()
    {
        fireManager.Fire();
    }

    public void Update()
    {
        float horizontal = InputManager.Instance.horizontal; // 이건 바꿔야함 잠와서 아직안바꿈
        
        if (horizontal > 0f)
        {
            boosterL?.SetActive(true);
            boosterR?.SetActive(false);
        }
        else if (horizontal < 0f)
        {
            boosterL?.SetActive(false);
            boosterR?.SetActive(true);
        }
        else
        {
            boosterL?.SetActive(false);
            boosterR?.SetActive(false);
        }

        if (fireManager.Curruntammo() <= 0)
        {
            rightAnimator.SetBool("isSpaceAmmo", false);
            leftAnimator.SetBool("isSpaceAmmo", false);
            
            isReturnToBasic = true;
            isMachingunEquip = false;

            fireManager.SetBulletType(BulletType.Basic);
            fireManager.SetMuzzle(muzzleMiddle);
        }
        
    }

    public void Exit()
    {
        
    }

    public void TakeHit()
    {
        hp--;
        Debug.Log($"[SpaceShip] 피격! 남은 체력: {hp}");
        EventManager.Instance.InvokeHitEffect(rightRenderer, hitColor);
        EventManager.Instance.InvokeHitEffect(leftRenderer, hitColor);
        
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hitSound);

        if (hp <= 0)
        {
            player.KillPlayer();
        }
    }
    
    
    public void OnPickupItem()
    {
        fireManager.UpgradeMuzzles(muzzleLeftRight);
        fireManager.SetBulletType(BulletType.MachinGun);
        if (!isMachingunEquip)
        {
            rightAnimator.SetBool("isSpaceAmmo", true);
            leftAnimator.SetBool("isSpaceAmmo", true);
            isMachingunEquip = true;
        }
    }
    private void GetComponents()
    {
        Transform right = spaceShip.transform.Find("SpaceShip_Right_0");
        Transform left = spaceShip.transform.Find("SpaceShip_Left_0");

        if (right != null && left != null)
        {
            right.TryGetComponent(out rightAnimator);
            left.TryGetComponent(out leftAnimator);
            
            right.TryGetComponent(out rightRenderer);
            left.TryGetComponent(out leftRenderer);
        }
        
    }
}
