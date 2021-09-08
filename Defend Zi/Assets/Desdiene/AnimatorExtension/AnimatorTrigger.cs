using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public struct AnimatorTrigger
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;

        public AnimatorTrigger(Animator animator, AnimatorParameters parameters, string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (!_parameters.Has(_paramName, AnimatorControllerParameterType.Trigger))
            {
                throw new ArgumentNullException(nameof(_paramName), $"Trigger param was not found in animator {_animator.name}");
            }
        }

        public void Trigger() => _animator.SetTrigger(_paramName);
    }
}
