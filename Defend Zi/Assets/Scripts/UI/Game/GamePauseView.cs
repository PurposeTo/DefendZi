using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class GamePauseView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _pauseScreen;

    public event Action OnResumeClicked;
    public event Action OnMainMenuClicked;

    public void Enable()
    {
        _pauseScreen.SetActive(true);
    }

    public void Disable()
    {
        _pauseScreen.SetActive(false);
    }

    // вызывается при нажатии на кнопку движком юнити
    private void InvokeOnResumeClicked() => OnResumeClicked?.Invoke();

    // вызывается при нажатии на кнопку движком юнити
    private void InvokeOnMainMenuClicked() => OnMainMenuClicked?.Invoke();
}
