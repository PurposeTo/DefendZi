﻿using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.Container;
using UnityEngine;

namespace Desdiene.Coroutine.CoroutineExecutor
{
    /*
     * ICoroutineInfo, в методах, запускающих корутину, всегда идут с ref параметром потому, что
     * таким образом будет гарантия того, что ТОЧНО будет переназначена ссылка на объект ICoroutineInfo
     */
    public class CoroutineExecutor : MonoBehaviourContainer
    {
        public CoroutineExecutor(MonoBehaviour monoBehaviour) : base(monoBehaviour) { }


        private readonly List<ICoroutine> allCoroutineContainers = new List<ICoroutine>();


        public ICoroutine CreateCoroutineContainer()
        {
            return new CoroutineWithData();
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

            CoroutineWithData coroutineWithData = (CoroutineWithData)coroutineInfo;
            coroutineWithData.SetEnumerator(enumerator);

            if (!coroutineWithData.IsExecuting)
            {
                StartNewCoroutine(coroutineWithData);
            }
            else coroutineWithData.OnAlreadyStarted?.Invoke();
        }


        /// <summary>
        /// Перед запуском корутины останавливает её, если она выполнялась на данный момент.
        /// </summary>
        /// <param name="enumerator">IEnumerator для выполнения</param>
        /// <returns></returns>
        public void ReStartCoroutineExecution(ICoroutine coroutineInfo, IEnumerator enumerator)
        {
            if (enumerator == null) throw new ArgumentNullException(nameof(enumerator));

            CoroutineWithData coroutineWithData = (CoroutineWithData)coroutineInfo;
            coroutineWithData.SetEnumerator(enumerator);

            if (coroutineInfo.IsExecuting) BreakCoroutine(coroutineInfo);

            StartNewCoroutine(coroutineWithData);
        }


        /// <summary>
        /// Останавливает корутину.
        /// </summary>
        /// <param name="coroutineInfo"></param>
        public void BreakCoroutine(ICoroutine coroutineInfo)
        {
            if (coroutineInfo == null) throw new ArgumentNullException(nameof(coroutineInfo));

            CoroutineWithData coroutineWithData = (CoroutineWithData)coroutineInfo;

            if (coroutineWithData.IsExecuting)
            {
                monoBehaviour.StopCoroutine(coroutineWithData.Coroutine);

                SetNullToCoroutineAndRemove(coroutineWithData);
            }
            else coroutineWithData.OnIsAlreadyStopped?.Invoke();

            coroutineWithData.OnStop?.Invoke();
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


        private void StartNewCoroutine(CoroutineWithData coroutineWithData)
        {
            coroutineWithData.SetCoroutine(monoBehaviour.StartCoroutine(WrappedEnumerator(coroutineWithData)));
            allCoroutineContainers.Add(coroutineWithData);
        }


        private IEnumerator WrappedEnumerator(CoroutineWithData coroutineWithData)
        {
            yield return coroutineWithData.Enumerator;
            SetNullToCoroutineAndRemove(coroutineWithData);
        }


        private void SetNullToCoroutineAndRemove(CoroutineWithData coroutineWithData)
        {
            coroutineWithData.SetNullToCoroutine();
            allCoroutineContainers.Remove(coroutineWithData);
        }


        private class CoroutineWithData : ICoroutine
        {
            public IEnumerator Enumerator { get; private set; } = null;
            public UnityEngine.Coroutine Coroutine { get; private set; } = null;
            public bool IsExecuting => Coroutine != null;


            public void SetCoroutine(UnityEngine.Coroutine coroutine)
            {
                Coroutine = coroutine ?? throw new ArgumentNullException(nameof(coroutine));
            }


            public void SetEnumerator(IEnumerator enumerator)
            {
                Enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
            }


            /// <summary>
            /// Выполняется во время выполнении метода ExecuteCoroutineContinuously, 
            /// в случае, если корутина уже была запущена.
            /// </summary>
            public Action OnAlreadyStarted { get; set; } = null;
            /// <summary>
            /// Выполняется во время выполнении метода BreakCoroutine,
            /// после остановки корутины.
            /// </summary>
            public Action OnStop { get; set; } = null;
            /// <summary>
            /// Выполняется во время выполнении метода BreakCoroutine,
            /// в случае, если корутина УЖЕ была остановлена.
            /// </summary>
            public Action OnIsAlreadyStopped { get; set; } = null;


            public void SetNullToCoroutine() => Coroutine = null;
        }
    }
}