using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : FullScreenWindow
{
    [SerializeField, NotNull] private Button _playButton;

    protected override void AwakeWindow()
    {
        _playButton.onClick.AddListener(() => OnGameClicked?.Invoke());
    }

    public event Action OnGameClicked;
}
