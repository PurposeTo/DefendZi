using UnityEngine;

public class ZiAura : MonoBehaviour
{
    private IPercentStat ziHealth;

    private PlayerAura playerAura;

    private readonly float minDeltaCharge = 0.1f;
    private readonly float maxDeltaCharge = 0.25f;

    public ZiAura Constructor(IPercentStat ziHealth)
    {
        this.ziHealth = ziHealth;
        SubscribeEvents();
        return this;
    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerAura playerAura))
        {
            this.playerAura = playerAura;
            EnableCharge();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerAura playerAura))
        {
            DisableCharge();
            this.playerAura = null;
        }
    }

    private void SubscribeEvents()
    {
        ziHealth.OnStatChange += EnableCharge;
    }


    private void UnsubscribeEvents()
    {
        ziHealth.OnStatChange -= EnableCharge;
    }


    private void EnableCharge()
    {
        if (playerAura != null)
        {
            playerAura.EnableCharging(Mathf.Lerp(maxDeltaCharge, minDeltaCharge, ziHealth.GetPercent()));
        }
    }

    private void DisableCharge()
    {
        playerAura.DisableCharging();
    }
}
