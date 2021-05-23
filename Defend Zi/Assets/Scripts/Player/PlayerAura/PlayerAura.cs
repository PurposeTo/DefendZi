using System;
using Desdiene.Types.AtomicReference;
using Desdiene.SuperMonoBehaviourAsset;
using UnityEngine;
using Desdiene.Types.ValuesInRange;
using Desdiene.Types.RangeType;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerAura : SuperMonoBehaviour
{
    public event Action OnIsChargingChange;
    public bool IsCharging => state.Get() is PlayerAuraCharge;

    private readonly Ref<PlayerAuraState> state = new Ref<PlayerAuraState>();

    private readonly Percent charge = new Percent(1);
    private FloatPercentable auraSize;

    protected override void AwakeWrapped()
    {
        Range<float> auraSizeRange = new Range<float>(1f, transform.localScale.x);
        auraSize = new FloatPercentable(transform.localScale.x, auraSizeRange);
        state.Set(new PlayerAuraDischarge(this, charge, auraSize));
        DisableCharging();
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
