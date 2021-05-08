using System;
using Desdiene.AtomicReference;
using Desdiene.SuperMonoBehaviourAsset;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerAura : SuperMonoBehaviour
{
    public event Action OnIsChargingChange;
    public bool IsCharging => state.Get() is PlayerAuraCharge;

    private readonly AtomicRef<PlayerAuraState> state = new AtomicRef<PlayerAuraState>();

    private protected PercentStat charge = new PercentStat(1);
    private protected FloatStatPercentable auraSize;

    protected override void AwakeWrapped()
    {
        auraSize = new FloatStatPercentable(transform.localScale.x, 1f, transform.localScale.x);
        state.Set(new PlayerAuraDischarge(this, charge, auraSize));
    }

    public void EnableCharging(float deltaCharge)
    {
        state.Get().EnableCharging(state, deltaCharge);
        OnIsChargingChange?.Invoke();
    }

    public void DisableCharging()
    {
        state.Get().DisableCharging(state);
        OnIsChargingChange?.Invoke();
    }
}
