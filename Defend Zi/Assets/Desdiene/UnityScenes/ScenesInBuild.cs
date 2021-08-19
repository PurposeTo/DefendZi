using System.IO;
using System.Linq;
using Desdiene.Singleton.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes
{
    /// <summary>
    /// Класс содержит информацию о сценах в сборке.
    /// Обращаться к SceneManager можно только из MonoBehaviour класса.
    /// </summary>
    public class ScenesInBuild : GlobalSingleton<ScenesInBuild>
    {
        private string[] _scenesInBuild;

        protected override void AwakeSingleton()
        {
            int sceneNumber = SceneManager.sceneCountInBuildSettings;
            string[] arrayOfNames;
            arrayOfNames = new string[sceneNumber];
            for (int i = 0; i < sceneNumber; i++)
            {
                arrayOfNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
            _scenesInBuild = arrayOfNames;
            Debug.Log($"Сцен в сборке: {_scenesInBuild.Length}. Имена сцен:\n{string.Join("\n", _scenesInBuild.ToArray())}");
        }

        public bool Contains(string sceneName) => _scenesInBuild.Contains(sceneName);

        public string[] GetNames() => _scenesInBuild;
    }
}
