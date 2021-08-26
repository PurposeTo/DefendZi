using System;
using UnityEngine;

namespace Desdiene.UnityScenes.Unloadings
{
    public class Unloading : IUnloading
    {
        private protected AsyncOperation _unloadingOperation;

        public Unloading(AsyncOperation unloadingOperation)
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
