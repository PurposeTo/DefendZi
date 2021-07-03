using Desdiene.MonoBehaviourExtension;
using UnityEngine;

[RequireComponent(typeof(Chunk))]
[ExecuteInEditMode]
public class GizmosChunkSize : MonoBehaviourExt
{
    private Chunk _chunk;

    private Vector2 _minimumCornerPosition;
    private Vector2 _minWidthMinimumCornerPosition;
    private Vector2 _maxWidthMinimumCornerPosition;

    protected override void AwakeExt()
    {
        _chunk = GetComponent<Chunk>();
    }

    private void OnDrawGizmos()
    {
        Init();

        if (Application.isPlaying) DrawCurrentSize();
        else
        {
            DrawBorderSize(_minWidthMinimumCornerPosition, _chunk.MinWidth);
            DrawBorderSize(_maxWidthMinimumCornerPosition, _chunk.MaxWidth);
        }
    }

    private void Init()
    {
        var position = transform.position;

        _minimumCornerPosition = new Vector2(position.x - _chunk.Width / 2, position.y - _chunk.Height / 2);
        _minWidthMinimumCornerPosition = new Vector2(position.x - _chunk.MinWidth / 2, position.y - _chunk.Height / 2);
        _maxWidthMinimumCornerPosition = new Vector2(position.x - _chunk.MaxWidth / 2, position.y - _chunk.Height / 2);
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
        Rect currentSize = new Rect(_minimumCornerPosition, new Vector2(_chunk.Width, _chunk.Height));
        DrawRectangle(Color.yellow, currentSize);
    }

    private void DrawBorderSize(Vector2 minimumCornerPosition, float width)
    {
        Color color = new Color(1f, 0.55f, 0.3f); // Orange
        Rect size = new Rect(minimumCornerPosition, new Vector2(width, _chunk.Height));
        DrawRectangle(color, size);
    }
}
