using UnityEngine;
using Bullet;

public class SpaceShipState : IPlayerState, IPlayerPickupReceiver
{
    private readonly GameObject spaceShip;
    private readonly FireManager fireManager;
    private readonly Player player;
    
    private readonly Transform muzzleMiddle;
    private readonly Transform[] muzzleLeftRight;
    
    private readonly GameObject boosterL;
    private readonly GameObject boosterR;

    private bool EquipMachingun = false;

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
    }

    public void Exit()
    {
        
    }

    public void OnPickupItem()
    {
        fireManager.UpgradeMuzzles(muzzleLeftRight);
        fireManager.SetBulletType(BulletType.MachinGun);
        if (EquipMachingun == false)
        {
            player.EquipMachingun();
            EquipMachingun = true;
        }
        else if (EquipMachingun == true)
        {
            
        }
        
    }
    
}
