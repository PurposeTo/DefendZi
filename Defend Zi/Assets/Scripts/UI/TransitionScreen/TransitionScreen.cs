using System;
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
[RequireComponent(typeof(TransitionScreenAnimator))]
public class TransitionScreen : ModalWindow
{
    private SceneLoader _sceneLoader;
    private TransitionScreenAnimator _animator;

    [Inject]
    public void Constructor(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
    }

    protected override void AwakeWindow()
    {
        _animator = GetInitedComponent<TransitionScreenAnimator>();
        SubscribeEvents();
        Hide();
    }

    protected override void OnDestroyWindow()
    {
        UnSubsctibeEvents();
    }

    protected override IProcessAccessorNotifier ShowWindow()
    {
       return _animator.Show();
    }

    protected override IProcessAccessorNotifier HideWindow()
    {
        return _animator.Hide();
    }

    private void Show(IProcessesMutator processes)
    {
        IProcessAccessorNotifier _wait = Show();
        processes.Add(_wait);
    }

    private void SubscribeEvents()
    {
        _sceneLoader.AfterEnabling += HideThis;
        _sceneLoader.BeforeUnloading += Show;
    }

    private void UnSubsctibeEvents()
    {
        _sceneLoader.AfterEnabling -= HideThis;
        _sceneLoader.BeforeUnloading -= Show;
    }

    private void HideThis() => Hide();
}
