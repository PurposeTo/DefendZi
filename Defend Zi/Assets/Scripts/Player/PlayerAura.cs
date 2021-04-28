using System;
using System.Collections;
using Desdiene.UnityEngineExtension;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerAura : MonoBehaviour
{
    public event Action OnIsChargingChange;
    public bool IsCharging { get; private set; }

    private readonly float minAuraValue = 1f;
    private readonly float maxAuraValue = 4f;

    private PercentStat charge = new PercentStat(1);

    private readonly float deltaDischarge = 0.05f;
    //todo должно передаваться от Zi
    private readonly float deltaCharge = 0.2f;

    private void Awake()
    {
        transform.SetLocalScale(GetAuraSizeViaCharging());
    }

    //TODO: апдейт для временного теста
    private void Update()
    {
        if (IsCharging)
        {
            if (!charge.IsMax())
            {
                charge += deltaCharge * Time.deltaTime;
                transform.SetLocalScale(GetAuraSizeViaCharging());
            }
        }
        else
        {
            if (!charge.IsMin())
            {
                charge -= deltaDischarge * Time.deltaTime;
                transform.SetLocalScale(GetAuraSizeViaCharging());
            }
        }
    }

    //TODO: сделать две стадии: аура заряжается и аура разряжается.
    public void EnableCharging()
    {
        IsCharging = true;
        OnIsChargingChange?.Invoke();
    }

    public void DisableCharging()
    {
        IsCharging = false;
        OnIsChargingChange?.Invoke();
    }

    public IEnumerator ChargeAura(float deltaCharge)
    {
        while (true)
        {
            yield return null;
            if (!charge.IsMax())
            {
                charge += deltaCharge * Time.deltaTime;
                transform.SetLocalScale(GetAuraSizeViaCharging());
            }
        }
    }

    public IEnumerator DischargeAura(float deltaDischarge)
    {
        while (true)
        {
            yield return null;
            if (!charge.IsMin())
            {
                charge -= deltaDischarge * Time.deltaTime;
                transform.SetLocalScale(GetAuraSizeViaCharging());
            }
        }
    }

    private float GetAuraSizeViaCharging()
    {
        return Mathf.Lerp(minAuraValue, maxAuraValue, charge.Value);
    }
}
