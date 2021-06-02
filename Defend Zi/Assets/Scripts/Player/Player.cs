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
    private IScoreCollector collector;
    private PlayerHealth health;

    protected override void AwakeExt()
    {
        IPosition position = GetComponent<PlayerPosition>();
        control = GetComponent<PlayerControl>().Constructor(position);
        health = GetComponent<PlayerHealth>();
        collector = GetComponent<PlayerScore>();
        GetComponent<ScoreAdderByTime>().Constructor(collector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D)
        {
            if (collision.TryGetComponent(out IDamageDealer damageDealer))
            {
                health.TakeDamage(damageDealer.GetDamage());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IScore score))
        {
            collector.Add(score.Value);
        }
    }

    void IUserControlled.Control(IUserInput input) => control.Control(input);
}
