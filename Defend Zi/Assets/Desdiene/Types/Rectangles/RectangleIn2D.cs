using System;
using Desdiene.Types.Ufloats;
using UnityEngine;

namespace Desdiene.Types.Rectangles
{
    /*
     * Невозможно использовать для редактирование через unity inspector:
     * Т.к. тогда изменяются значения полей, которое нельзя отследить, необходимо пересчитать всю фигуру.
     */
    /// <summary>
    /// Описывает прямоугольник в 2D пространстве.
    /// Pivot по умолчанию находится в центре.
    /// </summary>
    [Serializable]
    public struct RectangleIn2D : IRectangleIn2DAccessor
    {
        [SerializeField] private Ufloat _height;
        [SerializeField] private Ufloat _width;

        [SerializeField] private Vector2 _position;
        [SerializeField] private Vector2 _pivotOffset;

        [SerializeField] private Quaternion _rotation;

        public RectangleIn2D(Rectangle rectangle, Vector2 position, Quaternion rotation)
            : this(rectangle, position, rotation, Vector2.zero) { }

        public RectangleIn2D(Rectangle rectangle, Vector2 position, Quaternion rotation, Vector2 pivotOffset)
        {
            _height = rectangle.Height;
            _width = rectangle.Width;
            _position = position;
            _pivotOffset = pivotOffset;

            _rotation = rotation;
        }

        #region реализация интерфейсов через прокидывание ссылок на поле/свойство/метод

        float IRectangleAccessor.Height => _height;
        float IRectangleAccessor.Width => _width;
        Vector2 IPositionAccessor.Value => _position;
        Vector2 IPivotOffset2DAccessor.Value => _pivotOffset;
        float IRotationAccessor.Angle => _rotation.eulerAngles.z;
        Quaternion IRotationAccessor.Quaternion => _rotation;
        #endregion

        public float Height { get => _height; set => _height = value; }
        public float Width { get => _width; set => _width = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 PivotOffset { get => _pivotOffset; set => _pivotOffset = value; }
        public Quaternion Rotation { get => _rotation; set => _rotation = value; }

        public void CopyConfigsTo(BoxCollider2D boxCollider2D)
        {
            if (boxCollider2D == null) throw new ArgumentNullException(nameof(boxCollider2D));

            boxCollider2D.size = new Vector2(Width, Height);
            boxCollider2D.offset = PivotOffset;
        }

        public void CopyConfigsFrom(BoxCollider2D boxCollider2D)
        {
            if (boxCollider2D == null) throw new ArgumentNullException(nameof(boxCollider2D));
            Width = boxCollider2D.size.x;
            Height = boxCollider2D.size.y;
            PivotOffset = boxCollider2D.offset;
        }
    }
}
