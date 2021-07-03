using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class MovableChunkTrigger : MonoBehaviourExt
{
    private IMovableChunk movableChunk;

    protected override void AwakeExt()
    {
        movableChunk = GetInitedComponentOnlyInParent<IMovableChunk>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerSelection _))
        {
            movableChunk.Move();
        }
    }
}
