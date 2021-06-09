using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageDealerTracker : MonoBehaviourExt, IDamageDealer
{
    private IDamageDealer damageDealer;

    protected override void Constructor()
    {
        //todo: верное ли использование?
        damageDealer = GetComponentOnlyInParent<IDamageDealer>();
    }

    uint IDamageDealer.Get() => damageDealer.Get();
}
