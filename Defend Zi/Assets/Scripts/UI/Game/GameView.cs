using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class GameView : FullScreenWindow
{
    [SerializeField, NotNull] private Button _pauseButton;

    protected override void AwakeWindow()
    {
        _pauseButton.onClick.AddListener(() => OnPauseClicked?.Invoke());
    }

    public event Action OnPauseClicked;

}
