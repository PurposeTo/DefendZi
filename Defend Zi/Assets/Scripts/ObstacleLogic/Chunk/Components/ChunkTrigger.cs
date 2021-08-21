using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ChunkTrigger : MonoBehaviourExt
{
    [SerializeField, NotNull] private InterfaceComponent<ITriggerable> _triggerableChunk;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerSelection _))
        {
            _triggerableChunk.Implementation.Invoke();
        }
    }
}
