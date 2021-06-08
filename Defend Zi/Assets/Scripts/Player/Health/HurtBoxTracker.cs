using Desdiene.MonoBehaviourExtention;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBoxTracker : MonoBehaviourExt
{
    private IDamageTaker damageTaker;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        damageTaker = GetComponentInParent<IDamageTaker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageDealer damageDealer))
        {
            damageTaker.TakeDamage(damageDealer.Get());
        }
    }
}
