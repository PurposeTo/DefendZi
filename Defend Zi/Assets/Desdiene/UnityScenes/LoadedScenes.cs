using System;
using System.Linq;
using Desdiene.Singletons.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes
{
    /*
     * SceneManager.sceneCount считает все загруженные в данный момент сцены - без учитывания включены ли они.
     * Scene scene.isLoaded - вернет true тогда, когда сцена И загружена, И включена.
     * SceneManager.sceneLoaded - вызовет событие тогда, когда сцена будет И загружена, И включена.
     */
    /// <summary>
    /// Класс содержит информацию о текущих загруженных сценах.
    /// Обращаться к SceneManager можно только из MonoBehaviour класса.
    /// </summary>
    public class LoadedScenes : GlobalSingleton<LoadedScenes>
    {
        /// <summary>
        /// Получить массив загруженных сцен, без учета, включены ли они.
        /// </summary>
        public Scene[] Get()
        {
            int countLoaded = SceneManager.sceneCount;
            Scene[] loadedScenes = new Scene[countLoaded];

            for (int i = 0; i < countLoaded; i++)
            {
                loadedScenes[i] = SceneManager.GetSceneAt(i);
            }

            Debug.Log($"Имена загруженных сцен:\n{string.Join("\n", loadedScenes.Select(it => it.name))}");
            return loadedScenes;
        }

        public bool Contains(Scene scene) => Get().Contains(scene);

        public bool Contains(string sceneName)
        {
            Scene[] scenes = Get();
            return Array.Exists(scenes, scene => scene.name == sceneName);
        }
    }
}
