using System;
using Desdiene.UnityScenes.LoadingProcess.Components;

namespace Desdiene.UnityScenes.LoadingProcess
{
    public interface ILoading
    {
        /// <summary>
        /// Событие вызывается при включении состояния ожидания разрешения на активацию сцены
        /// </summary>
        event Action OnWaitingForAllowingToEnabling;

        /// <summary>
        /// Событие вызывается после загрузки и включении сцены.
        /// </summary>
        event Action OnLoadedAndEnabled;

        /// <summary>
        /// Установить разрешение на включение сцены после загрузки.
        /// Внимание! Загруженная, но не включенная сцена все равно учитывается unity как загруженная.
        /// </summary>
        void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode mode);
    }
}
