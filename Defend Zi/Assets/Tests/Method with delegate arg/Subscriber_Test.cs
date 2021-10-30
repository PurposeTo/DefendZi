using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class Subscriber_Test : MonoBehaviourExt
{
    [SerializeField, NotNull] Observable_Test _observable;

    protected override void AwakeExt()
    {
        _observable.OnTestEvent += () => Debug.Log($"{GetType().Name} call");
        // будет выброс исключения из-за обращения к Monobehaviour
        _observable.OnTestEvent += () => Debug.Log($"{gameObject} call");
        Destroy(gameObject);
    }
}
