using System;
using UnityEngine;

namespace Desdiene.UnityScenes.UnloadingProcess
{
    public class UnloadingOperation : IUnloading
    {
        private protected AsyncOperation _unloadingOperation;

        public UnloadingOperation(AsyncOperation unloadingOperation)
        {
            _unloadingOperation = unloadingOperation;
        }

        public event Action OnUnloaded
        {
            add => _unloadingOperation.completed += (_) => value?.Invoke();
            remove => _unloadingOperation.completed -= (_) => value?.Invoke();
        }
    }
}
