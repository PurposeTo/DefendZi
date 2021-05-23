using System.Collections;
using Desdiene.Types.AtomicReference;
using Desdiene.SuperMonoBehaviourAsset;
using UnityEngine;
using Desdiene.Types.ValuesInRange;

public class PlayerAuraCharge : PlayerAuraState
{
    public PlayerAuraCharge(SuperMonoBehaviour superMonoBehaviour, Percent charge, FloatPercentable auraSize)
        : base(superMonoBehaviour, charge, auraSize)
    {
        SetAuraSizeViaCharging();
    }

    public override void DisableCharging(Ref<PlayerAuraState> state)
    {
        superMonoBehaviour.BreakCoroutine(stateUpdate);
        state.Set(new PlayerAuraDischarge(superMonoBehaviour, charge, auraSize));
        state.Get().DisableCharging(state);
    }

    public override void EnableCharging(Ref<PlayerAuraState> state, float deltaCharge)
    {
        superMonoBehaviour.ReStartCoroutineExecution(stateUpdate, ChargeAura(deltaCharge));
    }

    public IEnumerator ChargeAura(float deltaCharge)
    {
        while (true)
        {
            if (!charge.IsMax())
            {
                charge.Set(charge + (deltaCharge * Time.deltaTime));
                SetAuraSizeViaCharging();
            }
            yield return null;
        }
    }
}
