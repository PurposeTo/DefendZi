using System;
using System.Collections;
using Desdiene.Container;
using Desdiene.Coroutine;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

namespace Desdiene.UnityScenes.LoadingProcess
{
    /// <summary>
    /// Данный класс описывает операцию асинхронной загрузки сцены.
    /// </summary>
    public class Loading : MonoBehaviourExtContainer
    {
        private readonly AsyncOperation _asyncOperation;

        public Loading(MonoBehaviourExt mono, AsyncOperation asyncOperation) : base(mono)
        {
            _asyncOperation = asyncOperation;
            new CoroutineWrap(mono).StartContinuously(CheckingStatus());
        }

        public float Progress => _asyncOperation.progress;

        /// <summary>
        /// Событие вызывается либо при смене на статус WaitingForAllowingToEnabling, либо если данный статус уже был пройден.
        /// </summary>
        public event Action OnWaitingForAllowingToEnabling
        {
            add
            {
                Status status = GetStatus();
                if (status >= Status.WaitingForAllowingToEnabling) value?.Invoke();
                else _onWaitingForAllowingToEnabling += value;
            }
            remove => _onWaitingForAllowingToEnabling -= value;
        }
        private Action _onWaitingForAllowingToEnabling;

        /// <summary>
        /// Событие вызывается либо при смене на статус LoadedAndEnabled, либо если данный статус уже присвоен.
        /// </summary>
        public event Action OnLoadedAndEnabled
        {
            add
            {
                Status status = GetStatus();
                if (status == Status.LoadedAndEnabled) value?.Invoke();
                else _asyncOperation.completed += (_) => value?.Invoke();
            }
            remove => _asyncOperation.completed -= (_) => value?.Invoke();
        }

        /* Используется слово "enable" для разделения понятий.
         * Согласно документации unity, может быть "active scene" - главная включенная сцена, при использовании LoadSceneMode.Additive
         * И может быть allowSceneActivation - разрешение включение сцены после загрузки.
         */
        /// <summary>
        /// Установить разрешение на включение сцены после загрузки.
        /// Внимание! Загруженная, но не включенная сцена все равно учитывается unity как загруженная.
        /// </summary>
        public void SetAllowSceneEnabling(AllowingSceneEnabling.Mode enablingMode)
        {
            Status status = GetStatus();
            bool isAllow = AllowingSceneEnabling.Check(enablingMode);
            switch (status)
            {
                case Status.Loading:
                    _asyncOperation.allowSceneActivation = isAllow;
                    break;
                case Status.WaitingForAllowingToEnabling:
                    if (isAllow) AllowSceneEnabling();
                    else Debug.LogWarning("Scene loading is already forbidden to enabling");
                    break;
                case Status.Enabling:
                    Debug.LogWarning($"You can't change scene loading allowing to {isAllow} while enabling");
                    break;
                case Status.LoadedAndEnabled:
                    Debug.LogWarning($"You can't change scene loading allowing to {isAllow}, because it is already loaded and enabled");
                    break;
                default:
                    throw new ArgumentException($"{status} is unknown status");
            }
        }

        /// <summary>
        /// Получить статус загрузки сцены.
        /// </summary>
        public Status GetStatus()
        {
            bool allowSceneActivation = _asyncOperation.allowSceneActivation;
            bool equals90Percents = Mathf.Approximately(Progress, 0.9f);
            bool equals100Percents = Mathf.Approximately(Progress, 1f);
            bool lessThan90Percents = Progress < 0.9f && !equals90Percents;
            bool equalsOrMoreThan90Percents = equals90Percents || Progress > 0.9f;
            bool equalsOrLessThan100Percents = equals100Percents || Progress < 1f;
            bool between90And100PercentsIncluding = equalsOrMoreThan90Percents || equalsOrLessThan100Percents;

            if (lessThan90Percents) return Status.Loading;
            else if (equals90Percents && !allowSceneActivation) return Status.WaitingForAllowingToEnabling;
            else if (between90And100PercentsIncluding && allowSceneActivation) return Status.Enabling;
            else if (_asyncOperation.isDone && equals100Percents && allowSceneActivation) return Status.LoadedAndEnabled;
            else
            {
                throw new InvalidOperationException($"Unknown loading status! Progress: {Progress * 100}%, " +
                    $"allowSceneActivation = {allowSceneActivation}, _asyncOperation.isDone = {_asyncOperation.isDone}");
            }
        }

        private IEnumerator CheckingStatus()
        {
            while (true)
            {
                var status = GetStatus();

                if (status == Status.WaitingForAllowingToEnabling)
                {
                    _onWaitingForAllowingToEnabling?.Invoke();
                    yield break;
                }
                else yield return null;
            }
        }

        private void AllowSceneEnabling() => _asyncOperation.allowSceneActivation = true;
        private void ForbidSceneEnabling() => _asyncOperation.allowSceneActivation = false;
    }
}
