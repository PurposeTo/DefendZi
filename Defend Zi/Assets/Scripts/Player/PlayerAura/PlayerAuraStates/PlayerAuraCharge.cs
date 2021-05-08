using System.Collections;
using Desdiene.AtomicReference;
using Desdiene.SuperMonoBehaviourAsset;
using UnityEngine;

public class PlayerAuraCharge : PlayerAuraState
{
    public PlayerAuraCharge(SuperMonoBehaviour superMonoBehaviour, PercentStat charge, FloatStatPercentable auraSize)
        : base(superMonoBehaviour, charge, auraSize)
    {
        SetAuraSizeViaCharging();
    }

    public override void DisableCharging(AtomicRef<PlayerAuraState> state)
    {
        superMonoBehaviour.BreakCoroutine(stateUpdate);
        state.Set(new PlayerAuraDischarge(superMonoBehaviour, charge, auraSize));
        state.Get().DisableCharging(state);
    }

    public override void EnableCharging(AtomicRef<PlayerAuraState> state, float deltaCharge)
    {
        superMonoBehaviour.ReStartCoroutineExecution(stateUpdate, ChargeAura(deltaCharge));
    }

    public IEnumerator ChargeAura(float deltaCharge)
    {
        while (true)
        {
            yield return null;
            if (!charge.IsMax())
            {
                charge.Set(charge + (deltaCharge * Time.deltaTime));
                SetAuraSizeViaCharging();
            }
        }
    }
}
