using System;
using UnityEngine;

public class PlayerActivity : MonoBehaviour
{
    public event Action OnActivityChanged;
    public bool IsActive { get; private set; } 
    
    private PlayerMovement playerMovement;
    private PlayerAura playerAura;


    public PlayerActivity Constructor(PlayerMovement playerMovement, PlayerAura playerAura)
    {
        this.playerMovement = playerMovement;
        this.playerAura = playerAura;
        SubscribeEvents();
        return this;
    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        playerMovement.OnIsUnderControlChange += SetIsActive;
        playerAura.OnIsChargingChange += SetIsActive;
    }


    private void UnsubscribeEvents()
    {
        playerMovement.OnIsUnderControlChange -= SetIsActive;
        playerAura.OnIsChargingChange -= SetIsActive;
    }

    private void SetIsActive()
    {
        bool isUnderControl = playerMovement.IsUnderControl;
        bool isPlayerAuraCharging = playerAura.IsCharging;

        IsActive = isUnderControl || isPlayerAuraCharging;
        OnActivityChanged?.Invoke();
    }
}
