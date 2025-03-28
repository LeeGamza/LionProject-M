using UnityEngine;
using Bullet;

public class HumanState : IPlayerState, IPlayerPickupReceiver, IDamageable
{
    private readonly GameObject humanPlayer;
    private readonly FireManager fireManager;
    private readonly Player player;
    
    private readonly Transform muzzleMiddle;
    
    private Animator pistolHumanAnimator;
    private Animator humanBooster;
    
    private GameObject booster;
    private GameObject pistolHuman;
    
    private bool isMachingunEquip = false;
    
    public HumanState(GameObject humanPlayer, 
        FireManager fireManager, 
        Transform muzzleMiddle, 
        Player player)
    {
        this.humanPlayer = humanPlayer;
        this.fireManager = fireManager;
    
        this.muzzleMiddle = muzzleMiddle;
        this.player = player;
        
        pistolHuman = humanPlayer.transform.Find("H_Player_Pistol")?.gameObject;
        booster = humanPlayer.transform.Find("Booster_M")?.gameObject;
    }
    
    public void Enter()
    {
        humanPlayer.SetActive(true);
        fireManager.SetBulletType(BulletType.Basic);
        fireManager.SetMuzzle(muzzleMiddle);
        
        GetComponents();
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        float horizontal = player.GetInputValue();
        
        if (horizontal > 0f)
        {
            pistolHumanAnimator.SetBool("isRight",true);
            pistolHumanAnimator.SetBool("isLeft",false);
            
            humanBooster.SetBool("isLeft", true);
            humanBooster.SetBool("isRight", false);
        }
        else if (horizontal < 0f)
        {
            pistolHumanAnimator.SetBool("isRight",false);
            pistolHumanAnimator.SetBool("isLeft",true);
            
            humanBooster.SetBool("isLeft", false);
            humanBooster.SetBool("isRight", true);
        }
        else
        {
            pistolHumanAnimator.SetBool("isRight",false);
            pistolHumanAnimator.SetBool("isLeft",false);
            
            humanBooster.SetBool("isLeft", false);
            humanBooster.SetBool("isRight", false);
        }
        
        if (fireManager.Curruntammo() <= 0)
        {
            pistolHumanAnimator.SetBool("isEquip", false);
            
            isMachingunEquip = false;

            fireManager.SetBulletType(BulletType.Basic);
        }
    }

    public void Attack()
    {
        fireManager.Fire();
    }

    public void TakeHit()
    {
        player.KillPlayer();
    }

    public void OnPickupItem()
    {
        fireManager.SetBulletType(BulletType.MachinGun);
        if (!isMachingunEquip)
        {
            pistolHumanAnimator.SetBool("isEquip", true);
            isMachingunEquip = true;
        }

    }

    private void GetComponents()
    {
        if (pistolHuman != null)
        {
            pistolHuman.TryGetComponent(out pistolHumanAnimator);
        }

        if (booster != null)
        {
            booster.TryGetComponent(out humanBooster);
        }
    }
}
