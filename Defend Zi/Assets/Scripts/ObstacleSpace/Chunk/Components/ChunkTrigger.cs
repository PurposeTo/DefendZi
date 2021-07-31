using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ChunkTrigger : MonoBehaviourExt
{
    private ITriggerable _triggerableChunk;

    protected override void AwakeExt()
    {
        _triggerableChunk = GetInitedComponentOnlyInParent<ITriggerable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerSelection _))
        {
            _triggerableChunk.Invoke();
        }
    }
}
