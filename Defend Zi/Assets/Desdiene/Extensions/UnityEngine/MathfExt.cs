namespace Desdiene.Extensions.UnityEngine
{
    public struct MathfExt
    {
        public static float ClampMin(float value, float minValue)
        {
            return value < minValue
                ? minValue
                : value;
        }

        public static float ClampMin(int value, int minValue)
        {
            return value < minValue
                ? minValue
                : value;
        }

        public static float ClampMax(float value, float maxValue)
        {
            return value > maxValue
                ? maxValue
                : value;
        }

        public static float ClampMax(int value, int maxValue)
        {
            return value > maxValue
                ? maxValue
                : value;
        }
    }
}
