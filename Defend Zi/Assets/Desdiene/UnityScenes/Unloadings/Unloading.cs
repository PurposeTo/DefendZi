using System;
using UnityEngine;

namespace Desdiene.UnityScenes.Unloadings
{
    public class Unloading : IUnloading
    {
        private protected AsyncOperation _unloadingByUnity;

        public Unloading(AsyncOperation unloadingByUnity)
        {
            _unloadingByUnity = unloadingByUnity ?? throw new ArgumentNullException(nameof(unloadingByUnity));
            SubscribeEvents();
        }

        private Action onUnloaded;

        event Action IUnloading.OnUnloaded
        {
            add { lock (this) { onUnloaded += value; } }
            remove { lock (this) { onUnloaded -= value; } }
        }

        private void InvokeOnUnloaded(AsyncOperation _) => onUnloaded?.Invoke();

        private void SubscribeEvents()
        {
            _unloadingByUnity.completed += InvokeOnUnloaded;
        }

        private void UnsubscribeEvents()
        {
            _unloadingByUnity.completed -= InvokeOnUnloaded;
        }
    }
}
