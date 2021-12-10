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
            _routine.StartContinuously(Routine(_fixedUpdateAction));
        }

        public static IEnumerator Routine(Action<float> action) => Routine(() => true, action);

        /// <summary>
        /// IEnumerator, который имитирует FixedUpdate.
        /// </summary>
        public static IEnumerator Routine(Func<bool> predicate, Action<float> action)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var wait = new WaitForFixedUpdate();

            while (predicate.Invoke())
            {
                float deltaTime = Time.fixedDeltaTime;
                action.Invoke(deltaTime);
                yield return wait;
            }
        }

        public static IEnumerator RealTimeRoutine(Action<float> action) => RealTimeRoutine(() => true, action);

        /// <summary>
        /// IEnumerator, который имитирует FixedUpdate в RealTime (Не зависит от Time.Scale).
        /// </summary>
        public static IEnumerator RealTimeRoutine(Func<bool> predicate, Action<float> action)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            var wait = new WaitForFixedUpdate();

            while (predicate.Invoke())
            {
                float deltaTime = Time.fixedUnscaledDeltaTime;
                action.Invoke(deltaTime);
                yield return wait;
            }
        }
    }
}
