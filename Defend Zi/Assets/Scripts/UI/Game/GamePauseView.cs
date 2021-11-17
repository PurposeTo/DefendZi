using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseView : FullScreenWindow
{
    [SerializeField, NotNull] private Button _resumeButton;
    [SerializeField, NotNull] private Button _mainMenuButton;
    // [SerializeField, NotNull] private UiToggle _soundMutingToggle;

    private bool _soundMuteState; // it's stub

    protected override void AwakeWindow()
    {
        _resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
        // _soundMutingToggle.OnChanged += (value) => OnSoundMuteChanged?.Invoke(value);
    }

    public event Action OnResumeClicked;
    public event Action OnMainMenuClicked;
    public event Action<bool> OnSoundMuteChanged;

    public bool SoundMutingToggleEnabled => _soundMuteState;
    //   public bool SoundMutingToggleEnabled => _soundMutingToggle.IsOn;

    public void SetSoundMutingToggleState(bool mute) => _soundMuteState = mute;
    //public void SetSoundMutingToggleState(bool mute) => _soundMutingToggle.SetState(mute);

}
