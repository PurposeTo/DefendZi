using System;

namespace Desdiene.UnityScenes.Loadings
{
    public interface ILoadingAndEnabling
    {
        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        event Action OnLoadedAndEnabled;
    }
}
