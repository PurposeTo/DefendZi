using System;
using Desdiene.UnityScenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneTypes.Base
{
    /// <summary>
    /// Дочернему классу необходимо дать название, соответствующему названию сцены.
    /// </summary>
    public abstract class SceneType
    {
        private readonly string _sceneName;

        public SceneType(ScenesInBuild scenesInBuild)
        {
            string sceneName = GetType().Name;

            if (!scenesInBuild.Contains(sceneName))
            {
                throw new NullReferenceException($"Scene with name {sceneName} not found in build!");
            }

            _sceneName = sceneName;

            Debug.Log($"Scene with name \"{_sceneName}\" was found successfully");
        }

        // todo: может ли сцена быть загружена несколько раз одновременно?

        public bool IsLoading => throw new NotImplementedException();
        public bool IsLoaded=> throw new NotImplementedException();

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void LoadAsync()
        {
            throw new NotImplementedException();
        }

        public void Unload()
        {
            throw new NotImplementedException();
        }

        public void UnloadAsync()
        {
            throw new NotImplementedException();
        }
    }
}

