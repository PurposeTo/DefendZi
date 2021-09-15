using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public class AnimatorInt
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;
        private readonly AnimatorControllerParameter _parameter;

        public AnimatorInt(Animator animator, AnimatorParameters parameters, string paramName, int expectedDefaultValue)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (_parameters.Has(_paramName, AnimatorControllerParameterType.Int, out AnimatorControllerParameter param))
            {
                _parameter = param;
            }
            else throw new ArgumentNullException(_paramName, $"Integer param was not found in animator \"{_animator.name}\"");

            ValidateDefaultValue(expectedDefaultValue);
        }

        public int Value
        {
            get => _animator.GetInteger(_paramName);
            set => _animator.SetInteger(_paramName, value);
        }
        private int DefaultValue { get => _parameter.defaultInt; set => _parameter.defaultInt = value; }


        public static implicit operator int(AnimatorInt aInt)
        {
            return aInt.Value;
        }

        private void ValidateDefaultValue(int expectedDefaultValue)
        {
            int actualDefaultValue = DefaultValue;
            if (actualDefaultValue == expectedDefaultValue) return;
            else
            {
                Debug.LogWarning($"Integer in animator \"{_animator.name}\" with name \"{_paramName}\" has incorrect default value: {actualDefaultValue}. Expected: {expectedDefaultValue}. The value will be changed to the expected one.");
                DefaultValue = expectedDefaultValue;
                Value = expectedDefaultValue;
            }
        }
    }
}
