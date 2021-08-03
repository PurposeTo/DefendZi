using Desdiene.MonoBehaviourExtension;
using Desdiene.Types.RectangleAsset;
using UnityEngine;

/// <summary>
/// Отрисовывает прямоугольник в 2D плоскости. (значение Z у плоскости = 0)
/// </summary>
[RequireComponent(typeof(IRectangleIn2DGetter))]
[ExecuteInEditMode]
public class GizmoBoxCollider2D : MonoBehaviourExt
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

    private void Draw(Color color, IRectangleIn2DGetter rectangleIn2D)
    {
        if (rectangleIn2D is null) throw new System.ArgumentNullException(nameof(rectangleIn2D));

        IPositionGetter rectPosition = rectangleIn2D;
        IPivotOffset2DGetter rectPivotOffset = rectangleIn2D;
        IRotationGetter rectRotation = rectangleIn2D;
        Vector3 position = rectPosition.Value + rectPivotOffset.Value;
        Vector3 size = new Vector3(rectangleIn2D.Width, rectangleIn2D.Height, 1f);

        /* Если используется изменение матрицы по TRS, то: 
         * 1. Необходимо обязательно задать все три параметра с использованием значений.
         * В противном случае, если задать например только поворот без позиции, то ось поворота "съедет" куда-то вбок.
         * 2. Так как позиция УЖЕ задается в матрице, ее не надо указывать в методе Gizmos.Draw - иначе она будет посчитана 2 раза
         */
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(position, rectRotation.Quaternion, transform.lossyScale);
        Gizmos.color = color;
        Gizmos.matrix = rotationMatrix;
        Gizmos.DrawWireCube(Vector3.zero, size);
    }
}
