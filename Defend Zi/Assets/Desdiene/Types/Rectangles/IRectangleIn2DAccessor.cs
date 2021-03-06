/// <summary>
/// Описывает прямоугольник, расположенный в 2D пространстве.
/// 
/// Применение: прямоугольник, расположенный в пространстве.
/// Зная Pivot и Position объекта, мы можем вычислить координаты точек данного прямоугольника.
/// </summary>
namespace Desdiene.Types.Rectangles
{
    public interface IRectangleIn2DAccessor : IRectangleAccessor, IPositionAccessor, IPivotOffset2DAccessor, IRotationAccessor
    {

    }
}
