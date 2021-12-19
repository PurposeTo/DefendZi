using System;
using System.Collections;
using Desdiene.Coroutines;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtension
{
    /// <summary>
    /// Позволяет использовать Update внутри не монобех классов
    /// </summary>
    [Obsolete]
    public class Update : MonoBehaviourExtContainer
    {
        private readonly ICoroutine _routine;
        private readonly Action<float> _updateAction;

        public Update(MonoBehaviourExt mono, Action action) : this(mono, (deltaTime) => action?.Invoke())
        { }

        public Update(MonoBehaviourExt mono, Action<float> action) : base(mono)
        {
            _routine = new CoroutineWrap(MonoBehaviourExt);
            _updateAction = action ?? throw new ArgumentNullException(nameof(action));
            _routine.StartContinuously(Routine(_updateAction));
        }

        public static IEnumerator Routine(Action<float> action) => Routine(() => true, action);

        /// <summary>
        /// IEnumerator, который имитирует Update.
        /// </summary>
        public static IEnumerator Routine(Func<bool> predicate, Action<float> action)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            while (predicate.Invoke())
            {
                float deltaTime = Time.deltaTime;
                action.Invoke(deltaTime);
                yield return null;
            }
        }

        public static IEnumerator RealTimeRoutine(Action<float> action) => RealTimeRoutine(() => true, action);

        /// <summary>
        /// IEnumerator, который имитирует Update в RealTime (Не зависит от Time.Scale).
        /// </summary>
        public static IEnumerator RealTimeRoutine(Func<bool> predicate, Action<float> action)
        {
            if (predicate is null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }

            while (predicate.Invoke())
            {
                float deltaTime = Time.unscaledDeltaTime;
                action.Invoke(deltaTime);
                yield return null;
            }
        }
    }
}
