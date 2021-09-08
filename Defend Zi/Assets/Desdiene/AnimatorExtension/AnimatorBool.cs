using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public struct AnimatorBool
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;

        public AnimatorBool(Animator animator, AnimatorParameters parameters, string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (!_parameters.Has(_paramName, AnimatorControllerParameterType.Bool))
            {
                throw new ArgumentNullException(nameof(_paramName), $"Bool param was not found in animator {_animator}");
            }
        }

        public bool Value
        {
            get => _animator.GetBool(_paramName);
            set => _animator.SetBool(_paramName, value);
        }

        public static implicit operator bool(AnimatorBool aBool)
        {
            return aBool.Value;
        }
    }
}
