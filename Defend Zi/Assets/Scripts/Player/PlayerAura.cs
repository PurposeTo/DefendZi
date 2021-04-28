using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class PlayerAura : MonoBehaviour
{
    public event Action OnIsChargingChange;
    public bool IsCharging { get; private set; }

    private CircleCollider2D circleCollider2D;

    private float minAuraValue = 0.5f;
    private float maxAuraValue = 2f;

    private void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    public void EnableCharging()
    {
        IsCharging = true;
    }

    public void DisableCharging()
    {
        IsCharging = false;
    }
}
