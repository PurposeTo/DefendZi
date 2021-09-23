using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;
using UnityEngine.UI;

public class GameOverView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _screen;
    [SerializeField, NotNull] private TextView _bestScoreText;
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _reloadLvlButton;

    protected override void AwakeExt()
    {
        _reloadLvlButton.onClick.AddListener(() => OnReloadLvlClicked?.Invoke());
    }

    public event Action OnReloadLvlClicked;

    public void Show(int score, int bestScore)
    {
        _screen.SetActive(true);
        SetScore(score);
        SetBestScore(bestScore);
    }

    public void Hide()
    {
        _screen.SetActive(false);
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
