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
        playerMovement.ZiPlayerDistance.OnValueChanged += SetIsActive;
    }


    private void UnsubscribeEvents()
    {
        playerMovement.ZiPlayerDistance.OnValueChanged -= SetIsActive;
    }

    private void SetIsActive()
    {
        //IsActive = playerMovement.ZiPlayerDistance.IsMin() || playerMovement.ZiPlayerDistance.IsMax();
        OnActivityChanged?.Invoke();
    }
}
