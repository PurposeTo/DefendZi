using System;
using System.Collections;
using Desdiene.Coroutines;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtension
{
    /// <summary>
    /// Позволяет использовать FixedUpdate внутри не монобех классов
    /// </summary>
    public class FixedUpdate : MonoBehaviourExtContainer
    {
        private readonly ICoroutine _routine;
        private readonly Action<float> _fixedUpdateAction;

        public FixedUpdate(MonoBehaviourExt mono, Action<float> action) : base(mono)
        {
            _routine = new CoroutineWrap(MonoBehaviourExt);
            _fixedUpdateAction = action ?? throw new ArgumentNullException(nameof(action));
            _routine.StartContinuously(FixedUpdateEnumerator());
        }

        private IEnumerator FixedUpdateEnumerator()
        {
            var wait = new WaitForFixedUpdate();

            while (true)
            {
                float deltaTime = Time.fixedDeltaTime;
                _fixedUpdateAction.Invoke(deltaTime);
                yield return wait;
            }
        }
    }
}
