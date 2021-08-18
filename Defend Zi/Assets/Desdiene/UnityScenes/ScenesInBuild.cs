using System.IO;
using System.Linq;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Desdiene.UnityScenes
{
    public class ScenesInBuild : MonoBehaviourExt
    {
        private string[] _scenesInBuild;

        protected override void AwakeExt()
        {
            _scenesInBuild = GetScenesInBuildNames();
            Debug.Log($"Сцен в сборке: {_scenesInBuild.Length}. Имена сцен:\n{string.Join("\n", _scenesInBuild.ToArray())}");
        }

        public bool Contains(string sceneName) => _scenesInBuild.Contains(sceneName);

        private string[] GetScenesInBuildNames()
        {
            int sceneNumber = SceneManager.sceneCountInBuildSettings;
            string[] arrayOfNames;
            arrayOfNames = new string[sceneNumber];
            for (int i = 0; i < sceneNumber; i++)
            {
                arrayOfNames[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            }
            return arrayOfNames;
        }
    }
}
