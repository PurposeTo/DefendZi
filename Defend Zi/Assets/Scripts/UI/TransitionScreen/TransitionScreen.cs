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

    protected override void ShowWindow(Action show)
    {
        _animator.Show();

        void OnDisplayed()
        {
            show.Invoke();
            _animator.OnDisplayed -= OnDisplayed;
        }
        _animator.OnDisplayed += OnDisplayed;

    }

    protected override void HideWindow(Action hide)
    {
        _animator.Hide();

        void OnHidden()
        {
            hide.Invoke();
            _animator.OnHidden -= OnHidden;
        }
        _animator.OnHidden += OnHidden;
    }

    private void Show(IProcessesMutator processes)
    {
        IProcess _waitForShow = new LinearProcess("Включение окна перехода между сценами");
        _waitForShow.Start();
        Show();

        void StopWaiting()
        {
            _waitForShow.Stop();
            WhenDisplayed -= StopWaiting;
        }

        WhenDisplayed += StopWaiting;
        processes.Add(_waitForShow);
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
