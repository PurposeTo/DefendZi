using Desdiene.Singleton.Unity;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes
{
    /// <summary>
    /// Класс содержит информацию о текущих загруженных сценах.
    /// Обращаться к SceneManager можно только из MonoBehaviour класса.
    /// </summary>
    public class LoadedScenes : GlobalSingleton<LoadedScenes>
    {
        /// <summary>
        /// Получить массив загруженных сцен.
        /// Учитывает также сцены, которые загружены, но не включены (AsyncOperation async.allowSceneActivation = false)
        /// </summary>
        public Scene[] Get()
        {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }

            return loadedScenes;
        }
    }
}
