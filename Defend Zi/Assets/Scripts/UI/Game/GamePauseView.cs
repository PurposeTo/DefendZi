using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseView : FullScreenWindow
{
    [SerializeField, NotNull] private Button _resumeButton;
    [SerializeField, NotNull] private Button _mainMenuButton;

    protected override void AwakeWindow()
    {
        _resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
    }

    public event Action OnResumeClicked;
    public event Action OnMainMenuClicked;
}
