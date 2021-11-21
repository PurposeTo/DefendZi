using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : FullScreenWindow
{
    [SerializeField, NotNull] private Button _playButton;
    [SerializeField, NotNull] private Button _leaderboardButton;
    [SerializeField, NotNull] private UiToggle _soundMutingToggle;
    [SerializeField, NotNull] private TextView _bestScore;
    [SerializeField, NotNull] private TextView _bestLifeTime;
    [SerializeField, NotNull] private TextView _averageLifeTime;

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

    public void SetSoundMutingToggleState(bool mute) => _soundMutingToggle.SetState(mute);

    public void SetBestScore(uint value)
    {
        _bestScore.SetText($"{value}");
    }

    public void SetBestLifeTime(TimeSpan value)
    {
        int valueSec = Mathf.RoundToInt((float)value.TotalSeconds);
        _bestLifeTime.SetText($"{valueSec}");
    }

    public void SetAverageLifeTime(TimeSpan value)
    {
        int valueSec = Mathf.RoundToInt((float)value.TotalSeconds);
        _averageLifeTime.SetText($"{valueSec}");
    }
}
