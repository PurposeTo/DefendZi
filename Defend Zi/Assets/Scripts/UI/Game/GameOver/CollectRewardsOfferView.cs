using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class CollectRewardsOfferView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _mainMenuButton;
    [SerializeField, NotNull] private Button _collectRewardsButton;

    protected override void AwakeWindow()
    {
        _mainMenuButton.onClick.AddListener(() => OnMainMenuClicked?.Invoke());
        _collectRewardsButton.onClick.AddListener(() => OnCollectRewards?.Invoke());
    }

    public event Action OnMainMenuClicked;
    public event Action OnCollectRewards;

    public void Init(int score)
    {
        SetScore(score);
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
