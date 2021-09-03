using System;

namespace Desdiene.UnityScenes.Loadings
{
    public interface ILoadingNotifier
    {
        /// <summary>
        /// Событие вызывается при включении состояния ожидания разрешения на активацию сцены
        /// </summary>
        event Action<ILoadingNotifier> OnLoaded;

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        event Action<ILoadingNotifier> OnLoadedAndEnabled;
    }
}
