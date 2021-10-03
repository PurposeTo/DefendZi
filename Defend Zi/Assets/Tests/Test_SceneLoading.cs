﻿using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;
using Desdiene.UnityScenes;
using SceneTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Test_SceneLoading : MonoBehaviourExt
{
    private SceneAsset _sceneType;
    private SceneLoader _sceneLoader;

    private readonly ILinearProcess _testWait = new LinearProcess("Тестовое ожидание");

    [Inject]
    private void Constructor(SceneLoader sceneLoader, ScenesInBuild scenesInBuild)
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
           // SceneObject firstLoadedScene = _loadedScenes.Get()[0];
            _sceneLoader.Reload();
        }

        if (Input.GetKeyDown(KeyCode.G)) _testWait.Stop();
    }

    private void BeforeUnloading(ILinearProcessesMutator processes)
    {
        _testWait.Start();
        processes.Add(_testWait);
    }

    private void AfterEnabling()
    {

    }
}
