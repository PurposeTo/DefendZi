using System;
using Desdiene.MonoBehaviourExtension;
using UnityEngine;

public class CollectRewardsOfferView : MonoBehaviourExt
{
    [SerializeField, NotNull] private GameObject _gameOverScreen;
    [SerializeField, NotNull] private TextView _scoreText;

    public void Enable(int score)
    {
        _gameOverScreen.SetActive(true);
        SetScore(score);
    }

    public void Disable()
    {
        _gameOverScreen.SetActive(false);
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
