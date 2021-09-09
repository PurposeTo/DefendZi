using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public struct AnimatorFloat
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;
        private readonly AnimatorControllerParameter _parameter;

        public AnimatorFloat(Animator animator, AnimatorParameters parameters, string paramName, float expectedDefaultValue)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (_parameters.Has(_paramName, AnimatorControllerParameterType.Float, out AnimatorControllerParameter param))
            {
                _parameter = param;
            }
            else throw new ArgumentNullException(nameof(_paramName), $"Float param was not found in animator \"{_animator.name}\"");

            ValidateDefaultValue(expectedDefaultValue);
        }

        public float Value
        {
            get => _animator.GetFloat(_paramName);
            set => _animator.SetFloat(_paramName, value);
        }

        private float DefaultValue { get => _parameter.defaultFloat; set => _parameter.defaultFloat = value; }

        public static implicit operator float(AnimatorFloat aFloat)
        {
            return aFloat.Value;
        }

        private void ValidateDefaultValue(float expectedDefaultValue)
        {
            float actualDefaultValue = DefaultValue;
            if (actualDefaultValue == expectedDefaultValue) return;
            else
            {
                Debug.LogWarning($"Float in animator \"{_animator.name}\" with name \"{_paramName}\" has incorrect default value: {actualDefaultValue}. Expected: {expectedDefaultValue}. The value will be changed to the expected one.");
                DefaultValue = expectedDefaultValue;
                Value = expectedDefaultValue;
            }
        }
    }
}
