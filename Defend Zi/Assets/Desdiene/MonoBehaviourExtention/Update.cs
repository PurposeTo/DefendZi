using System;
using System.Collections;
using Desdiene.Coroutines;
using UnityEngine;

namespace Desdiene.MonoBehaviourExtension
{
    /// <summary>
    /// Позволяет использовать Update внутри не монобех классов
    /// </summary>
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
            _routine.StartContinuously(UpdateEnumerator());
        }

        private IEnumerator UpdateEnumerator()
        {
            float deltaTime = Time.deltaTime;
            while (true)
            {
                _updateAction.Invoke(deltaTime);
                yield return null;
            }
        }
    }
}
