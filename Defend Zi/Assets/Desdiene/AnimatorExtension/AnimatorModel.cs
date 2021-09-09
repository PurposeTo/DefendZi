using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.AnimatorExtension
{
    /// <summary>
    /// Позволяет удобнее работать с аниматором.
    /// Использовать как модель.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public abstract class AnimatorModel : MonoBehaviourExt
    {
        private Animator _animator;
        private AnimatorParameters _animatorParameters;

        protected sealed override void AwakeExt()
        {
            _animator = GetComponent<Animator>();
            _animatorParameters = new AnimatorParameters(_animator);
            AwakeAnimator();
        }

        protected abstract void AwakeAnimator();

        protected AnimatorBool GetAnimatorBool(string paramName, bool expectedDefaultValue)
        {
            return new AnimatorBool(_animator, _animatorParameters, paramName, expectedDefaultValue);
        }

        protected AnimatorFloat GetAnimatorFloat(string paramName, float expectedDefaultValue)
        {
            return new AnimatorFloat(_animator, _animatorParameters, paramName, expectedDefaultValue);
        }

        protected AnimatorInt GetAnimatorInt(string paramName, int expectedDefaultValue)
        {
            return new AnimatorInt(_animator, _animatorParameters, paramName, expectedDefaultValue);
        }

        protected AnimatorTrigger GetAnimatorTrigger(string paramName)
        {
            return new AnimatorTrigger(_animator, _animatorParameters, paramName);
        }
    }
}
