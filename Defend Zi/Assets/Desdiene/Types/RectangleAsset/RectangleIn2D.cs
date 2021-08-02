﻿using UnityEngine;

namespace Desdiene.Types.RectangleAsset
{
    /*
     * Невозможно использовать для редактирование через unity inspector:
     * Т.к. тогда изменяются значения полей, которое нельзя отследить, необходимо пересчитать всю фигуру.
     */
    /// <summary>
    /// Описывает прямоугольник в 2D пространстве.
    /// </summary>
    public struct RectangleIn2D : IRectangleIn2DGetter
    {
        private float _height; // need to be ufloat
        private float _width; // need to be ufloat

        private Vector2 _leftBorder;
        private Vector2 _rightBorder;
        private Vector2 _bottomBorder;
        private Vector2 _upperBorder;

        private Vector2 _leftDown;
        private Vector2 _rightDown;
        private Vector2 _rightTop;
        private Vector2 _leftTop;

        private Vector2 _position;
        private Vector2 _pivotOffset;

        private Quaternion _rotation;

        public RectangleIn2D(Rectangle rectangle, Vector2 position, Quaternion rotation)
            : this(rectangle, position, rotation, Vector2.zero) { }

        public RectangleIn2D(Rectangle rectangle, Vector2 position, Quaternion rotation, Vector2 pivotOffset)
        {
            _height = rectangle.Height;
            _width = rectangle.Width;
            _position = position;
            _pivotOffset = pivotOffset;

            // нужно проинициализировать все поля, прежде чем использовать объект this
            _leftBorder = Vector2.zero;
            _rightBorder = Vector2.zero;
            _bottomBorder = Vector2.zero;
            _upperBorder = Vector2.zero;
            _leftDown = Vector2.zero;
            _rightDown = Vector2.zero;
            _rightTop = Vector2.zero;
            _leftTop = Vector2.zero;
            _rotation = rotation;

            Update();
        }

        #region реализация интерфейсов через прокидывание ссылок на поле/свойство/метод

        float IRectangleGetter.Height => _height;
        float IRectangleGetter.Width => _width;
        Vector2 IRectangleIn2DGetter.LeftBorder => _leftBorder;
        Vector2 IRectangleIn2DGetter.RightBorder => _rightBorder;
        Vector2 IRectangleIn2DGetter.BottomBorder => _bottomBorder;
        Vector2 IRectangleIn2DGetter.UpperBorder => _upperBorder;

        Vector2 IRectangleIn2DGetter.LeftDown => _leftDown;
        Vector2 IRectangleIn2DGetter.RightDown => _rightDown;
        Vector2 IRectangleIn2DGetter.RightTop => _rightTop;
        Vector2 IRectangleIn2DGetter.LeftTop => _leftTop;

        Vector2 IPositionGetter.Value => _position;
        Vector2 IPivotOffset2DGetter.Value => _pivotOffset;

        #endregion

        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                Update();
            }
        }
        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                Update();
            }
        }
        public Vector2 LeftBorder => _leftBorder;
        public Vector2 RightBorder => _rightBorder;
        public Vector2 BottomBorder => _bottomBorder;
        public Vector2 UpperBorder => _upperBorder;
        public Vector2 LeftDown => _leftDown;
        public Vector2 RightDown => _rightDown;
        public Vector2 RightTop => _rightTop;
        public Vector2 LeftTop => _leftTop;
        public Vector2 Position
        {
            get => _position;
            set
            {
                _position = value;
                Update();
            }
        }
        public Vector2 PivotOffset
        {
            get => _pivotOffset;
            set
            {
                _pivotOffset = value;
                Update();
            }
        }
        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                Update();
            }
        }

        private void Update()
        {
            //todo не учитывает поворот.
            UpdateBordersPosition();
            UpdateCornersPosition();
        }

        private void UpdateBordersPosition()
        {
            _leftBorder = _position - new Vector2((_width / 2) - _pivotOffset.x, 0f);
            _rightBorder = _position + new Vector2((_width / 2) - _pivotOffset.x, 0f);
            _bottomBorder = _position - new Vector2(0f, (_height / 2) - _pivotOffset.y);
            _upperBorder = _position + new Vector2(0f, (_height / 2) - _pivotOffset.y);
        }

        private void UpdateCornersPosition()
        {
            _leftDown = new Vector2(_leftBorder.x, _bottomBorder.y);
            _rightDown = new Vector2(_rightBorder.x, _bottomBorder.y);
            _rightTop = new Vector2(_rightBorder.x, _upperBorder.y);
            _leftTop = new Vector2(_leftBorder.x, _upperBorder.y);
        }
    }
}