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
    }

    private void OnDestroy()
    {
        PlayerPresenter.GetPlayerMovement().OnIsUnderControlChange -= SetIsActive;
    }

    private void SetIsActive()
    {
        bool isUnderControl = PlayerPresenter.GetPlayerMovement().IsUnderControl;

        IsActive = isUnderControl;
        OnActivityChanged?.Invoke();
    }
}
