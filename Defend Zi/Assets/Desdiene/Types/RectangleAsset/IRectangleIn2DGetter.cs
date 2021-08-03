using UnityEngine;

/// <summary>
/// Описывает прямоугольник, расположенный в 2D пространстве.
/// 
/// Применение: прямоугольник, расположенный в пространстве.
/// Зная Pivot и Position объекта, мы можем вычислить координаты точек данного прямоугольника.
/// </summary>
namespace Desdiene.Types.RectangleAsset
{
    public interface IRectangleIn2DGetter : IRectangleGetter, IPositionGetter, IPivotOffset2DGetter, IRotationGetter
    {

    }
}
