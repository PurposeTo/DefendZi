using UnityEngine;

public class ZiAura : MonoBehaviour
{
    private IStat<float> ziHealthPercent;

    private PlayerPresenter PlayerPresenter => GameObjectsHolder.Instance.PlayerPresenter;
    private readonly BoolStat isPlayerInAura = new BoolStat();

    private readonly float minDeltaCharge = 0.1f;
    private readonly float maxDeltaCharge = 0.25f;
    private float DeltaCharge => Mathf.Lerp(maxDeltaCharge, minDeltaCharge, ziHealthPercent.Value);

    public ZiAura Constructor(IStat<float> ziHealthPercent)
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
        GameObjectsHolder.InitializedInstance += (_) =>
        {
            PlayerMovement playerMovement = PlayerPresenter.Movement;
            playerMovement.OnAwaked += () =>
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
        isPlayerInAura.Set(PlayerPresenter.Movement.ZiPlayerDistance.Value > 0.6);
    }

    private void ToggleCharge()
    {
        PlayerAura playerAura = PlayerPresenter.Aura;
        if (isPlayerInAura) playerAura.EnableCharging(DeltaCharge);
        else playerAura.DisableCharging();
    }
}
