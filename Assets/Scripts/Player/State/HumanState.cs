using UnityEngine;
using DG.Tweening;
using Bullet;

public class HumanState : IPlayerState, IPlayerPickupReceiver, IDamageable
{
    private float invincibleHitTimer = 0f;
    private float invincibleHitInterval = 0.3f;
    
    private readonly GameObject humanPlayer;
    private readonly FireManager fireManager;
    private readonly Player player;
    private SpriteRenderer humanSprite;
    private readonly Color hitColor = new Color32(179,179,179,255);
    
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
        pistolHumanAnimator.SetBool("isDead", false);
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        float horizontal = player.GetInputValue();
        
        if (player.IsInvincible)
        {
            invincibleHitTimer += Time.deltaTime;

            if (invincibleHitTimer >= invincibleHitInterval)
            {
                invincibleHitTimer = 0f;
                EventManager.Instance.InvokeHitEffect(humanSprite, hitColor);
            }
        }
        else
        {
            invincibleHitTimer = 0f;
        }
        
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
        pistolHumanAnimator.SetTrigger("isDead");
        pistolHumanAnimator.SetBool("isEquip", false);
        fireManager.SetBulletType(BulletType.Basic);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.deathSound);
        
        if (player.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.gravityScale = 4f;
        }

        float deathTime = GetDeathAnimLength();
        DOVirtual.DelayedCall(deathTime, () =>
        {
            player.SetDead();
            player.KillPlayer();
        });
    }

    public void OnPickupItem()
    {
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
            pistolHuman.TryGetComponent(out humanSprite);
        }

        if (booster != null)
        {
            booster.TryGetComponent(out humanBooster);
        }
    }

    private float GetDeathAnimLength()
    {
        RuntimeAnimatorController ac = pistolHumanAnimator.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name == "H_Dead")
            {
                return clip.length;
            }
        }

        return 0.8f;
    }
}
