using System;

namespace Desdiene.UnityScenes.UnloadingProcess
{
    public interface IUnloading
    {
        /// <summary>
        /// Событие вызывается после выгрузки сцены.
        /// </summary>
        event Action OnUnloaded;
    }
}
