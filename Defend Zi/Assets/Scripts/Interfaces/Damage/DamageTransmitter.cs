using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageTransmitter : MonoBehaviourExt, IDamageDealer
{
    private IDamageDealer damageDealer;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        damageDealer = GetInitedComponentOnlyInParent<IDamageDealer>();
    }

    uint IDamageDealer.Value => damageDealer.Value;
}
