using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : FullScreenWindow
{
    [SerializeField, NotNull] private Button _playButton;
    [SerializeField, NotNull] private Button _leaderboardButton;

    protected override void AwakeWindow()
    {
        _playButton.onClick.AddListener(() => OnGameClicked?.Invoke());
        _leaderboardButton.onClick.AddListener(() => OnLeaderboardClicked?.Invoke());
    }

    public event Action OnGameClicked;
    public event Action OnLeaderboardClicked;
}
