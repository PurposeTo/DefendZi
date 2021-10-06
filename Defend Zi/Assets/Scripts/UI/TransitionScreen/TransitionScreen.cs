using System;
using System.Collections;
using System.Collections.Generic;
using Desdiene.MonoBehaviourExtension;
using Desdiene.SceneLoaders.Single;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;
using Desdiene.UI.Elements;
using UnityEngine;
using Zenject;

/// <summary>
/// Экран перехода между сценами.
/// need to be a global singleton.
/// </summary>
public class TransitionScreen : ModalWindow
{
    private SceneLoader _sceneLoader;
    private ToVisible _toVisible;
    private IProcess _waitForHide;

    [Inject]
    public void Constructor(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
    }

    protected override void AwakeWindow()
    {
        SubscribeEvents();
    }

    protected override void OnDestroyWindow()
    {
        UnSubsctibeEvents();
    }

    protected override void ShowWindow()
    {
        _toVisible.MakeTransparent();
    }

    protected override void HideWindow()
    {
        _toVisible.MakeTransparent();
    }

    private void Show(IProcessesMutator linearProcesses)
    {
        IProcess _waitForShow = new LinearProcess("Включение окна перехода между сценами");
        _waitForShow.Start();
        Show();
        linearProcesses.Add(_waitForShow);
    }

    private void SubscribeEvents()
    {
        _sceneLoader.AfterEnabling += Hide;
        _sceneLoader.BeforeUnloading += Show;
    }

    private void UnSubsctibeEvents()
    {
        _sceneLoader.AfterEnabling -= Hide;
        _sceneLoader.BeforeUnloading -= Show;
    }
}
