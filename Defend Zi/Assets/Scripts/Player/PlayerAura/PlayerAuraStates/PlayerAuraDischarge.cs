using System.Collections;
using Desdiene.SuperMonoBehaviourAsset;
using UnityEngine;
using Desdiene.Types.AtomicReference;
using Desdiene.Types.ValuesInRange;

public class PlayerAuraDischarge : PlayerAuraState
{
    public PlayerAuraDischarge(SuperMonoBehaviour superMonoBehaviour, Percent charge, FloatPercentable auraSize)
        : base(superMonoBehaviour, charge, auraSize) 
    {
        SetAuraSizeViaCharging();
    }

    private readonly float deltaDischarge = 0.05f;

    public override void DisableCharging(Ref<PlayerAuraState> state)
    {
        superMonoBehaviour.ExecuteCoroutineContinuously(stateUpdate, DischargeAura(deltaDischarge));
        return;
    }

    public override void EnableCharging(Ref<PlayerAuraState> state, float deltaCharge)
    {
        superMonoBehaviour.BreakCoroutine(stateUpdate);
        state.Set(new PlayerAuraCharge(superMonoBehaviour, charge, auraSize));
        state.Get().EnableCharging(state, deltaCharge);
    }

    public IEnumerator DischargeAura(float deltaDischarge)
    {
        while (true)
        {
            yield return null;
            if (!charge.IsMin())
            {
                charge.Set(charge - (deltaDischarge * Time.deltaTime));
                SetAuraSizeViaCharging();
            }
        }
    }
}
