using UnityEngine;

public class PlayerStateMachine
{
    private IPlayerState currentState;
    
    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
    
    public void Update()
    {
        currentState?.Update();
    }
    
    public void OnPickupItem()
    {
        if (currentState is IPlayerPickupReceiver pickupReceiver)
        {
            pickupReceiver.OnPickupItem();
        }
    }

    public IPlayerState CurrentState => currentState;
}
