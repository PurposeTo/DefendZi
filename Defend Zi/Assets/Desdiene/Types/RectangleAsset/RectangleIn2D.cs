using System;
using UnityEngine;

namespace Desdiene.Types.RectangleAsset
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
    public struct RectangleIn2D : IRectangleIn2DGetter
    {
        [SerializeField] private float _height; // need to be ufloat
        [SerializeField] private float _width; // need to be ufloat

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

        float IRectangleGetter.Height => _height;
        float IRectangleGetter.Width => _width;
        Vector2 IPositionGetter.Value => _position;
        Vector2 IPivotOffset2DGetter.Value => _pivotOffset;
        float IRotationGetter.Angle => _rotation.eulerAngles.z;
        Quaternion IRotationGetter.Quaternion => _rotation;
        #endregion

        public float Height { get => _height; set => _height = value; }
        public float Width { get => _width; set => _width = value; }
        public Vector2 Position { get => _position; set => _position = value; }
        public Vector2 PivotOffset { get => _pivotOffset; set => _pivotOffset = value; }
        public Quaternion Rotation { get => _rotation; set => _rotation = value; }
    }
}
