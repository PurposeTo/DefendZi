using Desdiene;
using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Отрисовывает прямоугольник в 2D плоскости. (значение Z у плоскости = 0)
/// </summary>
[RequireComponent(typeof(IRectangleIn2DGetter))]
[ExecuteInEditMode]
public class GizmoRectangleIn2D : MonoBehaviourExt
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

        GizmoDrawing.Draw(_color, _rect2DPointsPosition, transform.lossyScale);
    }
}
