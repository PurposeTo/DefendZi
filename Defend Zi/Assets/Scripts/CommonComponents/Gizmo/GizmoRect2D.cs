using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Отрисовывает прямоугольник в 2D плоскости. (значение Z у плоскости = 0)
/// </summary>
[RequireComponent(typeof(IRectangleIn2DGetter))]
[ExecuteInEditMode]
public class GizmoRect2D : MonoBehaviourExt
{
    private IRectangleIn2DGetter _rect2DPointsPosition;
    [SerializeField] private Color _color = Color.white;

    protected override void AwakeExt()
    {
        _rect2DPointsPosition = GetComponent<IRectangleIn2DGetter>();
    }

    private void OnDrawGizmos()
    {
        // так как выполняется в ExecuteInEditMode, AwakeExt может не выполнится. (А какого хуя он не выполняется?)
        if (_rect2DPointsPosition == null)
        {
            _rect2DPointsPosition = GetComponent<IRectangleIn2DGetter>();
        }

        Draw(_color, _rect2DPointsPosition);
    }

    private void Draw(Color color, IRectangleIn2DGetter rect2DPointsPosition)
    {
        if (rect2DPointsPosition is null) throw new System.ArgumentNullException(nameof(rect2DPointsPosition));

        Gizmos.color = color;
        Gizmos.DrawLine(_rect2DPointsPosition.LeftDown, _rect2DPointsPosition.RightDown);
        Gizmos.DrawLine(_rect2DPointsPosition.RightDown, _rect2DPointsPosition.RightTop);
        Gizmos.DrawLine(_rect2DPointsPosition.RightTop, _rect2DPointsPosition.LeftTop);
        Gizmos.DrawLine(_rect2DPointsPosition.LeftTop, _rect2DPointsPosition.LeftDown);
    }
}
