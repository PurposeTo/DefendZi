using UnityEngine;

/// <summary>
/// Описывает прямоугольник, расположенный в 2D пространстве.
/// 
/// Применение: прямоугольник, расположенный в пространстве.
/// Зная Pivot и Position объекта, мы можем вычислить координаты точек данного прямоугольника.
/// </summary>
namespace Desdiene.Types.RectangleAsset
{
    public interface IRectangleIn2DGetter : IRectangleGetter, IPositionGetter, IPivotOffset2DGetter
    {
        Vector2 LeftBorder { get; }
        Vector2 RightBorder { get; }
        Vector2 BottomBorder { get; }
        Vector2 UpperBorder { get; }

        Vector2 LeftDown { get; }
        Vector2 RightDown { get; }
        Vector2 RightTop { get; }
        Vector2 LeftTop { get; }
    }
}
