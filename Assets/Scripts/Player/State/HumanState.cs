using UnityEngine;
using Bullet;

public class HumanState : IPlayerState, IPlayerPickupReceiver, IDamageable
{
    private readonly GameObject humanPlayer;
    private readonly FireManager fireManager;
    private readonly Player player;
    
    private readonly Transform muzzleMiddle;
    
    private readonly GameObject boosterL;
    private readonly GameObject boosterR;
    
    public HumanState(GameObject humanPlayer, 
        FireManager fireManager, 
        Transform muzzleMiddle, 
        Player player)
    {
        this.humanPlayer = humanPlayer;
        this.fireManager = fireManager;
    
        this.muzzleMiddle = muzzleMiddle;
        this.player = player;
        
        /*boosterL = humanPlayer.transform.Find("Booster_L")?.gameObject;
        boosterR = humanPlayer.transform.Find("Booster_R")?.gameObject;*/
    }
    
    public void Enter()
    {
        humanPlayer.SetActive(true);
        fireManager.SetBulletType(BulletType.Basic);
        fireManager.SetMuzzle(muzzleMiddle);
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        // 움직임, 점프 등 입력 처리 가능
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
        
    }
}
