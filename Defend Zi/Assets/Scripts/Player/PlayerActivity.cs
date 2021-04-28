using System;
using UnityEngine;

public class PlayerActivity : MonoBehaviour
{
    public event Action OnActivityChanged;
    public bool IsActive { get; private set; } 
    private PlayerPresenter PlayerPresenter => GameObjectsHolder.Instance.PlayerPresenter;


    private void Awake()
    {
        PlayerPresenter.GetPlayerMovement().OnIsUnderControlChange += SetIsActive;
        PlayerPresenter.GetPlayerAura().OnIsChargingChange += SetIsActive;
    }

    private void OnDestroy()
    {
        PlayerPresenter.GetPlayerMovement().OnIsUnderControlChange -= SetIsActive;
        PlayerPresenter.GetPlayerAura().OnIsChargingChange -= SetIsActive;
    }

    private void SetIsActive()
    {
        bool isUnderControl = PlayerPresenter.GetPlayerMovement().IsUnderControl;
        bool isPlayerAuraCharging = PlayerPresenter.GetPlayerAura().IsCharging;

        IsActive = isUnderControl || isPlayerAuraCharging;
        OnActivityChanged?.Invoke();
    }
}
