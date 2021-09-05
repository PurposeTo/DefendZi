using System;

namespace Desdiene.UnityScenes.Loadings
{
    public interface ILoadingAndEnablingNotifier
    {
        /// <summary>
        /// Событие вызывается при включении состояния ожидания разрешения на активацию сцены
        /// </summary>
        event Action OnLoaded;

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        event Action OnLoadedAndEnabled;
    }
}
