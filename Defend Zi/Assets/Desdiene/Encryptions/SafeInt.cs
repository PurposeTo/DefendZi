using System;

namespace Desdiene.Encryptions
{
    public struct SafeInt
    {
        private readonly int _saltedValue;
        private readonly int _salt;

        public SafeInt(int value)
        {
            _salt = GetSalt();
            _saltedValue = value ^ _salt;
        }

        public SafeInt(SafeFloat safeIntValue)
        {
            _salt = GetSalt();
            _saltedValue = ((int)(float)safeIntValue) ^ _salt;
        }

        public override bool Equals(object obj)
        {
            return obj is SafeInt safeInt && this == safeInt;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return ((int)this).ToString();
        }


        public static implicit operator int(SafeInt safeInt)
        {
            return safeInt._saltedValue ^ safeInt._salt;
        }


        public static implicit operator SafeInt(int normalInt)
        {
            return new SafeInt(normalInt);
        }

        private static int GetSalt() => new Random().Next(int.MinValue / 4, int.MaxValue / 4);
    }
}