using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.Container;
using UnityEngine;

namespace Desdiene.Coroutine.CoroutineExecutor
{
    public class CoroutineExecutor : MonoBehaviourContainer
    {
        public CoroutineExecutor(MonoBehaviour monoBehaviour) : base(monoBehaviour) { }

        private readonly List<ICoroutine> allCoroutineContainers = new List<ICoroutine>();

        public ICoroutine CreateCoroutineContainer()
        {
            return new CoroutineData();
        }

        /// <summary>
        /// Запускает корутину в том случае, если она НЕ выполняется в данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        /// <returns></returns>
        public void ExecuteCoroutineContinuously(ICoroutine coroutineInfo, IEnumerator enumerator)
        {
            if (coroutineInfo == null) throw new ArgumentNullException(nameof(coroutineInfo));
            if (enumerator == null) throw new ArgumentNullException(nameof(enumerator));

            CoroutineData coroutineData = (CoroutineData)coroutineInfo;
            coroutineData.SetEnumerator(enumerator);

            if (!coroutineData.IsExecuting)
            {
                StartNewCoroutine(coroutineData);
            }
            else coroutineData.OnAlreadyStarted?.Invoke();
        }

        /// <summary>
        /// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        /// <returns></returns>
        public void ReStartCoroutineExecution(ICoroutine coroutineInfo, IEnumerator enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException(nameof(enumerator));

            CoroutineData coroutineData = (CoroutineData)coroutineInfo;
            BreakCoroutine(coroutineInfo);
            coroutineData.SetEnumerator(enumerator);
            StartNewCoroutine(coroutineData);
        }

        /// <summary>
        /// Останавливает корутину, если она выполнялась.
        /// </summary>
        /// <param name="coroutineInfo"></param>
        public void BreakCoroutine(ICoroutine coroutineInfo)
        {
            if (coroutineInfo == null) throw new ArgumentNullException(nameof(coroutineInfo));

            CoroutineData coroutineData = (CoroutineData)coroutineInfo;

            if (coroutineData.IsExecuting)
            {
                monoBehaviour.StopCoroutine(coroutineData.Coroutine);

                SetNullToCoroutineAndRemove(coroutineData);
            }
            else coroutineData.OnIsAlreadyStopped?.Invoke();

            coroutineData.OnStop?.Invoke();
        }

        /// <summary>
        /// Останавливает все корутины на объекте.
        /// </summary>
        /// <param name="coroutineInfo"></param>
        public void BreakAllCoroutines()
        {
            for (int i = 0; i < allCoroutineContainers.Count; i++)
            {
                ICoroutine coroutineContainer = allCoroutineContainers[i];

                BreakCoroutine(coroutineContainer);
            }
        }

        private void StartNewCoroutine(CoroutineData coroutineData)
        {
            coroutineData.SetCoroutine(monoBehaviour.StartCoroutine(WrappedEnumerator(coroutineData)));
            allCoroutineContainers.Add(coroutineData);
        }

        private IEnumerator WrappedEnumerator(CoroutineData coroutineData)
        {
            yield return coroutineData.Enumerator;
            SetNullToCoroutineAndRemove(coroutineData);
        }

        private void SetNullToCoroutineAndRemove(CoroutineData coroutineData)
        {
            coroutineData.SetNullToCoroutine();
            allCoroutineContainers.Remove(coroutineData);
        }
    }
}