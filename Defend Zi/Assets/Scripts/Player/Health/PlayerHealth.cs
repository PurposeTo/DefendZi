using System;
using Desdiene.Types.RangeType;
using Desdiene.Types.ValuesInRange;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerHealth : MonoBehaviour, IDamageTaker
{
    public event Action OnDie;
    private IntInRange health;

    private void Awake()
    {
        int defaultHealth = 1;
        health = new IntInRange(defaultHealth, new Range<int>(0, defaultHealth));
    }

    //todo: перенести в Player
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.TryGetComponent(out IDamageDealer damageDealer))
    //    {
    //        TakeDamage(damageDealer.GetDamage());
    //    }
    //}

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health == 0) Die();
    }

    private void Die()
    {
        OnDie?.Invoke();
    }
}
