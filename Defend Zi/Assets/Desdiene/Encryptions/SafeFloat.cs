using System;

namespace Desdiene.Encryptions
{
    public struct SafeFloat
    {
        private readonly int _saltedValue;
        private readonly int _salt;

        public SafeFloat(float value)
        {
            _salt = GetSalt();
            int intValue = BitConverter.ToInt32(BitConverter.GetBytes(value), 0);
            _saltedValue = intValue ^ _salt;
        }

        public override bool Equals(object obj)
        {
            return this == (float)obj;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return ((float)this).ToString();
        }

        public static implicit operator float(SafeFloat safeFloat)
        {
            return BitConverter.ToSingle(BitConverter.GetBytes(safeFloat._salt ^ safeFloat._saltedValue), 0);
        }

        public static implicit operator SafeFloat(float normalFloat)
        {
            return new SafeFloat(normalFloat);
        }

        public static explicit operator SafeInt(SafeFloat safeFloat)
        {
            return new SafeInt(safeFloat);
        }

        private static int GetSalt() => new Random().Next(int.MinValue / 4, int.MaxValue / 4);
    }
}