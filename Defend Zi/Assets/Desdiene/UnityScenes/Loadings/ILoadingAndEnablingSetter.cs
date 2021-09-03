using Desdiene.UnityScenes.Loadings.Components;

namespace Desdiene.UnityScenes.Loadings
{
    public interface ILoadingAndEnablingSetter
    {
        /// <summary>
        /// Установить разрешение на включение сцены после загрузки.
        /// </summary>
        void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode mode);
    }
}
