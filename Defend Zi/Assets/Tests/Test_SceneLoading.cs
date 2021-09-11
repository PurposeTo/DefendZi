using System;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.SceneTypes;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes;
using Desdiene.UnityScenes.Loadings;
using Desdiene.UnityScenes.Loadings.Components;
using Desdiene.UnityScenes.SceneTypes;
using Desdiene.UnityScenes.Unloadings;
using SceneTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Test_SceneLoading : MonoBehaviourExt
{
    private SceneAsset _sceneType;
    private SceneLoader _sceneLoader;

    private readonly IProcess _testWait = new Process("Тестовое ожидание");

    [Inject]
    private void Constructor(SceneLoader sceneLoader)
    {
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            Debug.Log($"Была загружена новая сцена!");
        };
        _sceneType = new Test(this);
        _sceneLoader = sceneLoader;
        _sceneLoader.BeforeUnloading += BeforeUnloading;
        _sceneLoader.AfterEnabling += AfterEnabling;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneObject firstLoadedScene = LoadedScenes.Instance.Get()[0];
            _sceneLoader.Reload();
        }

        if (Input.GetKeyDown(KeyCode.G)) _testWait.Complete();
    }

    private void BeforeUnloading(IProcessesSetter processes)
    {
        _testWait.Start();
        processes.Add(_testWait);
    }

    private void AfterEnabling()
    {

    }
}
