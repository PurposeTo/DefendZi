using UnityEngine;

/// <summary>
/// Описывает местоположение точек прямоугольника в 2D физике.
/// 
/// Применение: прямоугольник, расположенный в пространстве.
/// Зная Pivot и Position объекта, мы можем вычислить координаты точек данного прямоугольника.
/// </summary>
namespace Desdiene.Types.RectangleAsset
{
    public interface IRect2DPointsPosition
    {
        Vector2 LeftDown { get; }
        Vector2 RightDown { get; }
        Vector2 RightTop { get; }
        Vector2 LeftTop { get; }
    }
}
