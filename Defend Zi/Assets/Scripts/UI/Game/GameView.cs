using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class GameView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _gameScreen;

    public event Action OnPauseClicked;

    public void Enable()
    {
        _gameScreen.SetActive(true);
    }

    public void Disable()
    {
        _gameScreen.SetActive(false);
    }

    // вызывается при нажатии на кнопку движком юнити
    private void InvokeOnPauseClicked() => OnPauseClicked?.Invoke();
}
