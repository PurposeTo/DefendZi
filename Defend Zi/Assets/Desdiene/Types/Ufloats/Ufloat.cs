using System;
using UnityEngine;

namespace Desdiene.Types.Ufloats
{
    [Serializable]
    public struct Ufloat
    {
        [SerializeField] private float _value;

        public Ufloat(float value)
        {
            _value = Clamp(value);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return ((float)this).ToString();
        }


        public static implicit operator float(Ufloat ufloat)
        {
            return Clamp(ufloat._value);
        }

        public static implicit operator Ufloat(float normalFloat)
        {
            return new Ufloat(normalFloat);
        }

        public static Ufloat operator +(Ufloat first, Ufloat second)
        {
            return new Ufloat(first + second);
        }

        public static Ufloat operator -(Ufloat first, Ufloat second)
        {
            return new Ufloat(first - second);
        }

        public static Ufloat operator /(Ufloat first, Ufloat second)
        {
            return new Ufloat(first / second);
        }

        public static Ufloat operator *(Ufloat first, Ufloat second)
        {
            return new Ufloat(first * second);
        }

        public static bool operator ==(Ufloat first, Ufloat second)
        {
            return Mathf.Approximately(first, second);
        }

        public static bool operator !=(Ufloat first, Ufloat second)
        {
            return !(first == second);
        }

        private static float Clamp(float value) => Math.ClampMin(value, 0f);
    }
}
