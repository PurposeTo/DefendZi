using Desdiene.Types.AtomicReference;
using Desdiene.Container;
using Desdiene.Coroutine.CoroutineExecutor;
using Desdiene.Extensions.UnityEngine;
using Desdiene.SuperMonoBehaviourAsset;
using Desdiene.Types.ValuesInRange;

public abstract class PlayerAuraState : SuperMonoBehaviourContainer
{
    private protected readonly ICoroutineContainer stateUpdate;
    private protected readonly Percent charge;
    private protected readonly FloatPercentable auraSize;

    public PlayerAuraState(SuperMonoBehaviour superMonoBehaviour, Percent charge, FloatPercentable auraSize)
        : base(superMonoBehaviour)
    {
        stateUpdate = superMonoBehaviour.CreateCoroutineContainer();
        this.charge = charge;
        this.auraSize = auraSize;
    }

    public abstract void EnableCharging(Ref<PlayerAuraState> state, float deltaCharge);

    public abstract void DisableCharging(Ref<PlayerAuraState> state);

    private protected void SetAuraSizeViaCharging()
    {
        auraSize.SetByPercent(charge);
        superMonoBehaviour.transform.SetLocalScale(auraSize.Get());
    }
}
