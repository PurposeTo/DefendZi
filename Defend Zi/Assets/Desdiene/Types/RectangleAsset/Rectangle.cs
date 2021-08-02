﻿using System;
using UnityEngine;

namespace Desdiene.Types.RectangleAsset
{
    [Serializable]
    public struct Rectangle : IRectangle
    {
        public float Height { get => _height; set => _height = value; }
        public float Width { get => _width; set => _width = value; }

        //Не делать readonly, тк могут редактироваться через инспектор.
        [SerializeField] private float _height; // need to be ufloat
        [SerializeField] private float _width; // need to be ufloat

        public Rectangle(float height, float width)
        {
            _height = height;
            _width = width;
        }
    }
}
