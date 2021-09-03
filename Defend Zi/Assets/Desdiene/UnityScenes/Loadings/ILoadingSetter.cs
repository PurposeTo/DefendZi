using Desdiene.UnityScenes.Loadings.Components;

namespace Desdiene.UnityScenes.Loadings
{
    public interface ILoadingSetter
    {
        /// <summary>
        /// Установить разрешение на включение сцены после загрузки.
        /// </summary>
        void SetAllowSceneEnabling(SceneEnablingAfterLoading.Mode mode);
    }
}
