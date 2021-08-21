using System;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    [SerializeField, NotNull] private GameObject _gameOverScreen;
    [SerializeField, NotNull] private TextView _bestScoreText;
    [SerializeField, NotNull] private TextView _scoreText;

    public event Action OnReloadLvlClicked;

    public void Enable()
    {
        _gameOverScreen.SetActive(true);
    }

    public void Disable()
    {
        _gameOverScreen.SetActive(false);
    }

    public void SetBestScore(int bestScore)
    {
        _bestScoreText.SetText($"Best score: {bestScore}");
    }

    public void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }

    public void ReloadLvlButton()
    {
        OnReloadLvlClicked?.Invoke();
    }
}
