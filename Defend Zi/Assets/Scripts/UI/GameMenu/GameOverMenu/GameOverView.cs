using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _bestScoreText;
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _reloadLvlButton;

    protected override void AwakeWindow()
    {
        _reloadLvlButton.onClick.AddListener(() => OnReloadLvlClicked?.Invoke());
    }

    public event Action OnReloadLvlClicked;

    public void Init(int score, int bestScore)
    {
        SetScore(score);
        SetBestScore(bestScore);
    }

    private void SetBestScore(int bestScore)
    {
        _bestScoreText.SetText($"Best score: {bestScore}");
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
