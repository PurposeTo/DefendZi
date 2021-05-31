using System;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(PlayerPosition))]
[RequireComponent(typeof(PlayerControl))]
public class Player :
    MonoBehaviourExt,
    IUserControllable
{
    public event Action OnDied
    {
        add => health.OnDied += value;
        remove => health.OnDied -= value;
    }

    private IUserControllable control;
    private PlayerHealth health;

    protected override void AwakeExt()
    {
        IPosition position = GetComponent<PlayerPosition>();
        control = GetComponent<PlayerControl>().Constructor(position);
        health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageDealer damageDealer))
        {
            health.TakeDamage(damageDealer.GetDamage());
        }
    }

    void IUserControllable.Control(IUserInput input) => control.Control(input);
}
