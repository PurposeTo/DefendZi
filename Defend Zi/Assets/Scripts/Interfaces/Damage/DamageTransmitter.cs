using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageTransmitter : MonoBehaviourExt, IDamageDealer
{
    private IDamageDealer damageDealer;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        damageDealer = GetComponentOnlyInParent<IDamageDealer>();
    }

    uint IDamageDealer.Value => damageDealer.Value;
}
