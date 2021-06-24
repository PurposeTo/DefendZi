using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Chunk))]
[ExecuteInEditMode]
public class GizmosChunkSize : MonoBehaviourExt
{
    private Chunk _chunk;

    private Vector3 _leftDownCorner;
    private Vector3 _rightDownCorner;
    private Vector3 _rightTopCorner;
    private Vector3 _leftTopCorner;

    protected override void Constructor()
    {
        _chunk = GetComponent<Chunk>();
        InitSize();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(_leftDownCorner, _rightDownCorner);
        Gizmos.DrawLine(_rightDownCorner, _rightTopCorner);
        Gizmos.DrawLine(_rightTopCorner, _leftTopCorner);
        Gizmos.DrawLine(_leftTopCorner, _leftDownCorner);
    }

    private void InitSize()
    {
        var position = gameObject.transform.position;
        float width = _chunk.Width;
        float height = _chunk.Height;

        _leftDownCorner = new Vector3(position.x - width / 2, position.y - height / 2);
        _rightDownCorner = new Vector3(position.x + width / 2, position.y - height / 2);
        _rightTopCorner = new Vector3(position.x + width / 2, position.y + height / 2);
        _leftTopCorner = new Vector3(position.x - width / 2, position.y + height / 2);
    }
}
