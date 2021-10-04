using System;
using Desdiene.Types.Rectangles;
using UnityEngine;

namespace Desdiene
{
    public class GizmoDrawing
    {
        public static void Draw(Color color, IRectangleIn2DAccessor rectangleIn2D, Vector3 lossyScale)
        {
            if (rectangleIn2D is null) throw new ArgumentNullException(nameof(rectangleIn2D));

            IPositionAccessor rectPosition = rectangleIn2D;
            IPivotOffset2DAccessor rectPivotOffset = rectangleIn2D;
            IRotationAccessor rectRotation = rectangleIn2D;
            Vector3 position = rectPosition.Value + rectPivotOffset.Value;
            Vector3 size = new Vector3(rectangleIn2D.Width, rectangleIn2D.Height, 1f);

            /* Если используется изменение матрицы по TRS, то: 
             * 1. Необходимо обязательно задать все три параметра с использованием значений.
             * В противном случае, если задать например только поворот без позиции, то ось поворота "съедет" куда-то вбок.
             * 2. Так как позиция УЖЕ задается в матрице, ее не надо указывать в методе Gizmos.Draw - иначе она будет посчитана 2 раза
             */
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(position, rectRotation.Quaternion, lossyScale);
            Gizmos.color = color;
            Gizmos.matrix = rotationMatrix;
            Gizmos.DrawWireCube(Vector3.zero, size);
        }
    }
}
