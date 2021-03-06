using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _resumeButton;
    [SerializeField, NotNull] private Button _mainMenuButton;
    [SerializeField, NotNull] private UiToggle _soundMutingToggle;

    protected override void AwakeWindow()
    {
        _resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
        _soundMutingToggle.OnChanged += (value) => OnSoundMuteChanged?.Invoke(value);
    }

    public event Action OnResumeClicked;
    public event Action OnMainMenuClicked;
    public event Action<bool> OnSoundMuteChanged;

    public bool SoundMutingToggleEnabled => _soundMutingToggle.IsOn;

    public void SetSoundMutingToggleState(bool mute) => _soundMutingToggle.SetState(mute);

    public void Init(uint score)
    {
        SetScore(score);
    }

    private void SetScore(uint score)
    {
        _scoreText.SetText($"{score}");
    }
}
