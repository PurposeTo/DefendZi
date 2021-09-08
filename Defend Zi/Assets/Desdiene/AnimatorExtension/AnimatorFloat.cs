using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public struct AnimatorFloat
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;

        public AnimatorFloat(Animator animator, AnimatorParameters parameters, string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (!_parameters.Has(_paramName, AnimatorControllerParameterType.Float))
            {
                throw new ArgumentNullException(nameof(_paramName), $"Float param was not found in animator {_animator.name}");
            }
        }

        public float Value
        {
            get => _animator.GetFloat(_paramName);
            set => _animator.SetFloat(_paramName, value);
        }

        public static implicit operator float(AnimatorFloat aFloat)
        {
            return aFloat.Value;
        }
    }
}
