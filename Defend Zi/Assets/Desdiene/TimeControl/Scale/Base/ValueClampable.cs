using Desdiene.Extensions.UnityEngine;
using UnityEngine;

namespace Desdiene.TimeControl.Scale.Base
{
    public class ValueClampable
    {
        private readonly float minValue;
        private readonly float maxValue;

        public ValueClampable(float value, float minValue, float maxValue)
        {
            Value = value;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public float Value { get; private set; }

        public void SetClamped(float value)
        {
            Value = Mathf.Clamp(value, minValue, maxValue);
        }

        public void SetUnclaimedMax(float value)
        {
            Value = MathfExt.ClampMin(value, minValue);
        }
    }
}
