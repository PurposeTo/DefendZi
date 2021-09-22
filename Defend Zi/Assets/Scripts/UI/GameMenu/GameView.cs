using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class GameView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _gameScreen;

    public event Action OnPauseClicked;

    public void Show()
    {
        _gameScreen.SetActive(true);
    }

    public void Hide()
    {
        _gameScreen.SetActive(false);
    }

    // вызывается при нажатии на кнопку движком юнити
    public void InvokeOnPauseClicked() => OnPauseClicked?.Invoke();
}
