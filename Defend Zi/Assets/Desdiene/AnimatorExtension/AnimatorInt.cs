using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public struct AnimatorInt
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;

        public AnimatorInt(Animator animator, AnimatorParameters parameters, string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (!_parameters.Has(_paramName, AnimatorControllerParameterType.Int))
            {
                throw new ArgumentNullException(nameof(_paramName), $"Integer param was not found in animator {_animator.name}");
            }
        }

        public int Value
        {
            get => _animator.GetInteger(_paramName);
            set => _animator.SetInteger(_paramName, value);
        }

        public static implicit operator int(AnimatorInt aInt)
        {
            return aInt.Value;
        }
    }
}
