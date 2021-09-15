using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class MainMenuView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _mainMenuScreen;

    public event Action OnGameClicked;

    // вызывается при нажатии на кнопку движком юнити
    public void InvokeOnGameClicked() => OnGameClicked?.Invoke();
}
