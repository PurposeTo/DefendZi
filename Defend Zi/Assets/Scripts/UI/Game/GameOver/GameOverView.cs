using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _bestScoreText;
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _reloadLvlButton;
    [SerializeField, NotNull] private Button _mainMenuButton;

    protected override void AwakeWindow()
    {
        _reloadLvlButton.onClick.AddListener(() => OnReloadLvlClicked?.Invoke());
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
    }

    public event Action OnReloadLvlClicked;
    public event Action OnMainMenuClicked;

    public void Init(uint score, uint bestScore)
    {
        SetScore(score);
        SetBestScore(bestScore);
    }

    private void SetBestScore(uint bestScore)
    {
        _bestScoreText.SetText($"{bestScore}");
    }

    private void SetScore(uint score)
    {
        _scoreText.SetText($"{score}");
    }
}
