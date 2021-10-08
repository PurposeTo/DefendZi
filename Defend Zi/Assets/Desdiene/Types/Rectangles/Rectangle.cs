using System;
using Desdiene.Types.Ufloats;
using UnityEngine;

namespace Desdiene.Types.Rectangles
{
    [Serializable]
    public struct Rectangle : IRectangleAccessor
    {
        //Не делать readonly, тк могут редактироваться через инспектор.
        [SerializeField] private Ufloat _height;
        [SerializeField] private Ufloat _width;

        public Rectangle(float height, float width)
        {
            _height = height;
            _width = width;
        }

        #region реализация интерфейсов через прокидывание ссылок на поле/свойство/метод

        float IRectangleAccessor.Height => _height;
        float IRectangleAccessor.Width => _width;

        #endregion

        public float Height { get => _height; set => _height = value; }
        public float Width { get => _width; set => _width = value; }
    }
}
