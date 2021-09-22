using System;
using UnityEngine;

public class PlayerDeathView
{
    [SerializeField, NotNull] private GameObject _playerDeathScreen;
    [SerializeField, NotNull] private TextView _scoreText;

    public event Action OnCollectRewardsClicked;
    public event Action OnRevivePlayerClicked;

    public void Show(int score)
    {
        _playerDeathScreen.SetActive(true);
        SetScore(score);
    }

    public void Hide()
    {
        _playerDeathScreen.SetActive(false);
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }

    // вызывается при нажатии на кнопку движком unity
    public void CollectRewardsButton()
    {
        OnCollectRewardsClicked?.Invoke();
    }

    // вызывается при нажатии на кнопку движком unity
    public void RevivePlayerButton()
    {
        OnRevivePlayerClicked?.Invoke();
    }
}
