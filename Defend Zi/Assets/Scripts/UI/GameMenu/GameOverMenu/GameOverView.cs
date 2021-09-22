using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class GameOverView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _gameOverScreen;
    [SerializeField, NotNull] private TextView _bestScoreText;
    [SerializeField, NotNull] private TextView _scoreText;

    public event Action OnReloadLvlClicked;

    public void Show(int score, int bestScore)
    {
        _gameOverScreen.SetActive(true);
        SetScore(score);
        SetBestScore(bestScore);
    }

    public void Hide()
    {
        _gameOverScreen.SetActive(false);
    }

    private void SetBestScore(int bestScore)
    {
        _bestScoreText.SetText($"Best score: {bestScore}");
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }

    // вызывается при нажатии на кнопку движком unity
    public void ReloadLvlButton()
    {
        OnReloadLvlClicked?.Invoke();
    }
}
