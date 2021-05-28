using Desdiene.Types.AtomicReference;
using Desdiene.Types.ValuesInRange.Interfaces;
using UnityEngine;

public class ZiAura : MonoBehaviour
{
    private IReadPercentable ziHealthPercent;

    private PlayerPresenter PlayerPresenter => GameObjectsHolder.Instance.PlayerPresenter;
    private readonly Ref<bool> isPlayerInAura = new Ref<bool>();

    private readonly float minDeltaCharge = 0.1f;
    private readonly float maxDeltaCharge = 0.25f;
    private float DeltaCharge => Mathf.Lerp(maxDeltaCharge, minDeltaCharge, ziHealthPercent.GetPercent());

    public ZiAura Constructor(IReadPercentable ziHealthPercent)
    {
        this.ziHealthPercent = ziHealthPercent;
        SubscribeEvents();
        return this;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        isPlayerInAura.OnValueChanged += ToggleCharge;
        ziHealthPercent.OnValueChanged += CheckIfPlayerIsInAura;
        GameObjectsHolder.OnInited += (_) =>
        {
            PlayerMovement playerMovement = PlayerPresenter.Movement;
            playerMovement.OnIsAwaked += () =>
            {
                CheckIfPlayerIsInAura();
                playerMovement
                .ZiPlayerDistance
                .OnValueChanged += CheckIfPlayerIsInAura;
            };
        };

    }

    private void UnsubscribeEvents()
    {
        isPlayerInAura.OnValueChanged -= ToggleCharge;
        ziHealthPercent.OnValueChanged -= CheckIfPlayerIsInAura;
        PlayerPresenter.Movement.ZiPlayerDistance.OnValueChanged -= CheckIfPlayerIsInAura;
    }

    private void CheckIfPlayerIsInAura()
    {
        isPlayerInAura.Set(PlayerPresenter.Movement.ZiPlayerDistance.GetPercent() > 0.6);
    }

    private void ToggleCharge()
    {
        PlayerAura playerAura = PlayerPresenter.Aura;
        if (isPlayerInAura.Get()) playerAura.EnableCharging(DeltaCharge);
        else playerAura.DisableCharging();
    }
}
