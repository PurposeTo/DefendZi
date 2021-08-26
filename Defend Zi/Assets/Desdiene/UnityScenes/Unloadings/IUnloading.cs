using System;

namespace Desdiene.UnityScenes.Unloadings
{
    public interface IUnloading
    {
        /// <summary>
        /// Событие вызывается после выгрузки сцены.
        /// </summary>
        event Action OnUnloaded;
    }
}
