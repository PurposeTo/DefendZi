using Desdiene.AtomicReference;
using Desdiene.Container;
using Desdiene.Coroutine.CoroutineExecutor;
using Desdiene.SuperMonoBehaviourAsset;
using Desdiene.UnityEngineExtension;

public abstract class PlayerAuraState : SuperMonoBehaviourContainer
{
    private protected readonly ICoroutineContainer stateUpdate;
    private protected readonly PercentStat charge;
    private protected readonly FloatStatPercentable auraSize;

    public PlayerAuraState(SuperMonoBehaviour superMonoBehaviour, PercentStat charge, FloatStatPercentable auraSize)
        : base(superMonoBehaviour)
    {
        stateUpdate = superMonoBehaviour.CreateCoroutineContainer();
        this.charge = charge;
        this.auraSize = auraSize;
    }

    public abstract void EnableCharging(AtomicRef<PlayerAuraState> state, float deltaCharge);

    public abstract void DisableCharging(AtomicRef<PlayerAuraState> state);

    private protected void SetAuraSizeViaCharging()
    {
        superMonoBehaviour.transform.SetLocalScale(auraSize.SetByPercentAndGet(charge));
    }
}
