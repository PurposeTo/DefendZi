using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _gameScreen;
    [SerializeField, NotNull] private Button _pauseButton;

    protected override void AwakeExt()
    {
        _pauseButton.onClick.AddListener(() => OnPauseClicked?.Invoke());
    }

    public event Action OnPauseClicked;

    public void Show()
    {
        _gameScreen.SetActive(true);
    }

    public void Hide()
    {
        _gameScreen.SetActive(false);
    }
}
