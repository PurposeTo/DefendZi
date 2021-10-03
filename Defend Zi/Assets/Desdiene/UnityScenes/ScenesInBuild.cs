using System;
using System.IO;
using System.Linq;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes
{
    /// <summary>
    /// Класс содержит информацию о сценах в сборке.
    /// Обращаться к SceneManager можно только из MonoBehaviour класса.
    /// </summary>
    public sealed partial class ScenesInBuild : MonoBehaviourExt
    {
        private string[] _scenesInBuildNames;

        protected override void AwakeExt()
        {
            int sceneNumber = SceneManager.sceneCountInBuildSettings;
            string[] arrayOfNames;
            arrayOfNames = new string[sceneNumber];
            for (int i = 0; i < sceneNumber; i++)
            {
                arrayOfNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
            _scenesInBuildNames = arrayOfNames;
            Debug.Log($"Сцен в сборке: {_scenesInBuildNames.Length}. Имена сцен:\n{string.Join("\n", _scenesInBuildNames.ToArray())}");
        }

        public string[] GetNames() => _scenesInBuildNames;

        public SceneAsset Get(MonoBehaviourExt mono, string sceneName)
        {
            if (mono == null) throw new ArgumentNullException(nameof(mono));
            if (string.IsNullOrWhiteSpace(sceneName))
            {
                throw new ArgumentException($"\"{nameof(sceneName)}\" can't be null or empty", nameof(sceneName));
            }

            if (Contains(sceneName))
            {
                Debug.Log($"Scene with name \"{sceneName}\" was found successfully");
                return new SceneAsset(mono, sceneName);
            }
            else
            {
                throw new TypeLoadException($"Scene with name {sceneName} not found in build! " +
                    $"The class name must match the name of the existing scene and be unique");
            }
        }

        private bool Contains(string sceneName) => _scenesInBuildNames.Contains(sceneName);
    }
}
