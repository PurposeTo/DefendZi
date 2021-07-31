using Desdiene.MonoBehaviourExtension;
using UnityEngine;

// Отрисовывает UnityEngine.Rect с помощью Gizmo.
// Есть проблема - Pivot у Rect-а находится в левом верхнем углу.
public class DrawUnityRect : MonoBehaviourExt
{
    [SerializeField] private Color _color = Color.white;
    [SerializeField] private Rect rectangle;

    private void OnDrawGizmos()
    {
        Draw(_color, rectangle);
    }

    private void Draw(Color color, Rect rectangle)
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
}
