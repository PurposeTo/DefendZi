using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public class AnimatorBool
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;
        private readonly AnimatorControllerParameter _parameter;

        public AnimatorBool(Animator animator, AnimatorParameters parameters, string paramName, bool expectedDefaultValue)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (_parameters.Has(_paramName, AnimatorControllerParameterType.Bool, out AnimatorControllerParameter param))
            {
                _parameter = param;
            }
            else throw new ArgumentNullException(_paramName, $"Bool param was not found in animator \"{_animator.name}\"");

            ValidateDefaultValue(expectedDefaultValue);
        }

        public bool Value
        {
            get => _animator.GetBool(_paramName);
            set => _animator.SetBool(_paramName, value);
        }

        private bool DefaultValue { get => _parameter.defaultBool; set => _parameter.defaultBool = value; }

        public static implicit operator bool(AnimatorBool aBool)
        {
            return aBool.Value;
        }

        private void ValidateDefaultValue(bool expectedDefaultValue)
        {
            bool actualDefaultValue = DefaultValue;
            if (actualDefaultValue == expectedDefaultValue) return;
            else
            {
                Debug.LogWarning($"Bool in animator \"{_animator.name}\" with name \"{_paramName}\" has incorrect default value: {actualDefaultValue}. Expected: {expectedDefaultValue}. The value will be changed to the expected one.");
                DefaultValue = expectedDefaultValue;
                Value = expectedDefaultValue;
            }
        }
    }
}
