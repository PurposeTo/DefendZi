using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _collectRewardsButton;
    [SerializeField, NotNull] private Button _revivePlayerButton;

    public event Action OnCollectRewardsClicked;
    public event Action OnRevivePlayerClicked;

    protected override void AwakeWindow()
    {
        _collectRewardsButton.onClick.AddListener(() => OnCollectRewardsClicked?.Invoke());
        _revivePlayerButton.onClick.AddListener(() => OnRevivePlayerClicked?.Invoke());
    }

    public void Init(int score)
    {
        SetScore(score);
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
