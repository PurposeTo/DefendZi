using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _pauseScreen;
    [SerializeField, NotNull] private Button _resumeButton;
    [SerializeField, NotNull] private Button _mainMenuButton;

    protected override void AwakeExt()
    {
        _resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
    }

    public event Action OnResumeClicked;
    public event Action OnMainMenuClicked;

    public void Show()
    {
        _pauseScreen.SetActive(true);
    }

    public void Hide()
    {
        _pauseScreen.SetActive(false);
    }
}
