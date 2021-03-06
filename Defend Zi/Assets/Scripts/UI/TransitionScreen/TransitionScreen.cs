using System;
using Desdiene.SceneLoaders.Single;
using Desdiene.Types.ProcessContainers;
using Desdiene.Types.Processes;
using Desdiene.UI.Elements;
using Zenject;

/// <summary>
/// Экран перехода между сценами.
/// need to be a global singleton.
/// </summary>
public class TransitionScreen : ModalWindow
{
    private SceneLoader _sceneLoader;
    private readonly IProcess _waitForShowed = new CyclicalProcess($"Wait for hidding {typeof(TransitionScreen).Name}");

    [Inject]
    public void Constructor(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader ?? throw new ArgumentNullException(nameof(sceneLoader));
    }

    protected override void AwakeWindow()
    {
        SubscribeEvents();
        Hide();
    }

    protected override void OnDestroyWindow()
    {
        UnSubsctibeEvents();
    }

    protected override void ShowWindow()
    {
        _waitForShowed.Start();
    }

    private void Show(IProcessesMutator processes)
    {
        Show();
        processes.Add(_waitForShowed);
    }

    private void SubscribeEvents()
    {
        WhenDisplayed += _waitForShowed.Stop;
        _sceneLoader.AfterEnabling += Hide;
        _sceneLoader.BeforeUnloading += Show;
    }

    private void UnSubsctibeEvents()
    {
        WhenDisplayed -= _waitForShowed.Stop;
        _sceneLoader.AfterEnabling -= Hide;
        _sceneLoader.BeforeUnloading -= Show;
    }
}
