using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(IDamageTaker))]
public class DamageHitTracker : MonoBehaviourExt
{
    private IDamageTaker health;

    protected override void AwakeExt()
    {
        health = GetComponent<IDamageTaker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageDealer damageDealer))
        {
            health.TakeDamage(damageDealer.Get());
        }
    }
}
