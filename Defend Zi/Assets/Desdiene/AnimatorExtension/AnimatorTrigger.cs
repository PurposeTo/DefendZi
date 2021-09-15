using System;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    public class AnimatorTrigger
    {
        private readonly Animator _animator;
        private readonly AnimatorParameters _parameters;
        private readonly string _paramName;
        private readonly AnimatorControllerParameter _parameter;

        public AnimatorTrigger(Animator animator, AnimatorParameters parameters, string paramName)
        {
            if (string.IsNullOrEmpty(paramName))
            {
                throw new ArgumentException($"\"{nameof(paramName)}\" can't be null or empty", nameof(paramName));
            }

            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
            _parameters = parameters ?? throw new ArgumentNullException(nameof(parameters));
            _paramName = paramName;

            if (_parameters.Has(_paramName, AnimatorControllerParameterType.Trigger, out AnimatorControllerParameter param))
            {
                _parameter = param;
            }
            else throw new ArgumentNullException(_paramName, $"Trigger param was not found in animator \"{_animator.name}\"");
        }

        public void Trigger()
        {
            
            throw new NotImplementedException();

            //todo проверить реализацию на практике.

            /* Это надо сделать так:
             * 
             * _animator.SetTrigger(_paramName);
             * _animator.ResetTrigger(_paramName);
             */

            /* или так:
             * 
             * _parameters.ResetAllTriggers();
             * _animator.SetTrigger(_paramName);
             */
        }
    }
}
