using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DamageTransmitter : MonoBehaviourExt, IDamage
{
    private IDamage _damage;

    protected override void AwakeExt()
    {
        //todo: верное ли использование?
        _damage = GetComponentInParent<IDamage>();
    }

    uint IDamage.Value => _damage.Value;
}