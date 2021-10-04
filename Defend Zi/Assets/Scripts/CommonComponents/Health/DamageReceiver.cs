using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageReceiver : MonoBehaviourExt
{
    [SerializeField, NotNull] private InterfaceComponent<IDamageTaker> _damageTaker;

    private IDamageTaker DamageTaker => _damageTaker.Implementation;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamage damageDealer))
        {
            DamageTaker.TakeDamage(damageDealer);
            Debug.Log($"Receive by {gameObject.name} {damageDealer.Value} damage.");
        }
    }
}
