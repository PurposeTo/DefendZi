using System;
using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(PlayerPosition))]
[RequireComponent(typeof(PlayerControl))]
[RequireComponent(typeof(PlayerScore))]
[RequireComponent(typeof(ScoreAdderByTime))]
public class Player :
    MonoBehaviourExt,
    IUserControlled
{
    public event Action OnDied
    {
        add => health.OnDied += value;
        remove => health.OnDied -= value;
    }

    private IUserControlled control;
    private PlayerHealth health;

    protected override void AwakeExt()
    {
        IPosition position = GetComponent<PlayerPosition>();
        control = GetComponent<PlayerControl>().Constructor(position);
        health = GetComponent<PlayerHealth>();
        IScoreCollector collector = GetComponent<PlayerScore>();
        GetComponent<ScoreAdderByTime>().Constructor(collector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageDealer damageDealer))
        {
            health.TakeDamage(damageDealer.GetDamage());
        }
    }

    void IUserControlled.Control(IUserInput input) => control.Control(input);
}
