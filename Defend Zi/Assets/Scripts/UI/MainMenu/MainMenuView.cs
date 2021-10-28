using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : FullScreenWindow
{
    [SerializeField, NotNull] private Button _playButton;
    [SerializeField, NotNull] private Button _leaderboardButton;
    [SerializeField, NotNull] private UiToggle _soundMutingToggle;

    protected override void AwakeWindow()
    {
        _playButton.onClick.AddListener(() => OnGameClicked?.Invoke());
        _leaderboardButton.onClick.AddListener(() => OnLeaderboardClicked?.Invoke());
        _soundMutingToggle.OnChanged += (value) => OnSoundMuteChanged?.Invoke(value);
    }

    public event Action OnGameClicked;
    public event Action OnLeaderboardClicked;
    public event Action<bool> OnSoundMuteChanged;

    public bool SoundMutingToggleEnabled => _soundMutingToggle.IsOn;

    public void SetSoundMutingToggleState(bool enabled) => _soundMutingToggle.SetState(enabled);
}
