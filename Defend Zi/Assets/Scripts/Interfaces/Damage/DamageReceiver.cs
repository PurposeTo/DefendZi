using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageReceiver : MonoBehaviourExt
{
    private IDamageTaker _damageTaker;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        _damageTaker = GetComponentOnlyInParent<IDamageTaker>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageDealer damageDealer))
        {
            _damageTaker.TakeDamage(damageDealer.Value);
            Debug.Log($"Receive by {gameObject.name} {damageDealer.Value} damage.");
        }
    }
}
