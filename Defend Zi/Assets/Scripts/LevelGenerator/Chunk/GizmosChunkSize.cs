using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Chunk))]
[ExecuteInEditMode]
public class GizmosChunkSize : MonoBehaviourExt
{
    private Chunk _chunk;

    private Vector2 _minimumCornerPositionSpawnPlace;
    private Vector2 _minimumCornerPosition;

    protected override void AwakeExt()
    {
        _chunk = GetComponent<Chunk>();
    }

    private void OnDrawGizmos()
    {
        Init();
        DrawCurrentSize();
    }

    private void Init()
    {
        var position = transform.position;
        _minimumCornerPositionSpawnPlace = new Vector2(position.x - _chunk.SpawnPlaceWidth / 2, position.y - _chunk.Height / 2);
        _minimumCornerPosition = new Vector2(position.x - _chunk.Width / 2, position.y - _chunk.Height / 2);
    }

    private void DrawRectangle(Color color, Rect rectangle)
    {
        var leftDownCorner = new Vector2(rectangle.xMin, rectangle.yMin);
        var rightDownCorner = new Vector2(rectangle.xMax, rectangle.yMin);
        var rightTopCorner = new Vector2(rectangle.xMax, rectangle.yMax);
        var leftTopCorner = new Vector2(rectangle.xMin, rectangle.yMax);

        Gizmos.color = color;
        Gizmos.DrawLine(leftDownCorner, rightDownCorner);
        Gizmos.DrawLine(rightDownCorner, rightTopCorner);
        Gizmos.DrawLine(rightTopCorner, leftTopCorner);
        Gizmos.DrawLine(leftTopCorner, leftDownCorner);
    }

    private void DrawCurrentSize()
    {
        Rect currentSpawnPlaceSize = new Rect(_minimumCornerPositionSpawnPlace, new Vector2(_chunk.SpawnPlaceWidth, _chunk.Height));
        Rect currentSize = new Rect(_minimumCornerPosition, new Vector2(_chunk.Width, _chunk.Height));
        DrawRectangle(GetOrangeColor(), currentSpawnPlaceSize);
        DrawRectangle(Color.yellow, currentSize);
    }

    private Color GetOrangeColor() => new Color(1f, 0.55f, 0.3f);
}
