using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(IChunkSize))]
[ExecuteInEditMode]
public class GizmoChunkSize : MonoBehaviourExt
{
    private IChunkSize _chunkSize;

    private Vector2 _minimumCornerPositionSpawnPlace;
    private Vector2 _minimumCornerPosition;

    protected override void AwakeExt()
    {
        _chunkSize = GetComponent<IChunkSize>();
    }

    private void OnDrawGizmos()
    {
        Init();
        DrawCurrentSize();
    }

    private void Init()
    {
        var position = transform.position;
        _minimumCornerPositionSpawnPlace = new Vector2(position.x - _chunkSize.SpawnPlaceWidth / 2, position.y - _chunkSize.Height / 2);
        _minimumCornerPosition = new Vector2(position.x - _chunkSize.Width / 2, position.y - _chunkSize.Height / 2);
    }

    private void DrawCurrentSize()
    {
        Rect currentSpawnPlaceSize = new Rect(_minimumCornerPositionSpawnPlace, new Vector2(_chunkSize.SpawnPlaceWidth, _chunkSize.Height));
        Rect currentSize = new Rect(_minimumCornerPosition, new Vector2(_chunkSize.Width, _chunkSize.Height));
        DrawRectangle(GetOrangeColor(), currentSpawnPlaceSize);
        DrawRectangle(Color.yellow, currentSize);
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

    private Color GetOrangeColor() => new Color(1f, 0.55f, 0.3f);
}
