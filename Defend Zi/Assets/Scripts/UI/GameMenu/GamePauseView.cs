using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class GamePauseView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _pauseScreen;

    public event Action OnResumeClicked;
    public event Action OnMainMenuClicked;

    public void Show()
    {
        _pauseScreen.SetActive(true);
    }

    public void Hide()
    {
        _pauseScreen.SetActive(false);
    }

    // вызывается при нажатии на кнопку движком юнити
    public void InvokeOnResumeClicked() => OnResumeClicked?.Invoke();

    // вызывается при нажатии на кнопку движком юнити
    public void InvokeOnMainMenuClicked() => OnMainMenuClicked?.Invoke();
}
