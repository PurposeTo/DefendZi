using System;
using Desdiene.UI.Elements;
using UnityEngine;
using UnityEngine.UI;

public class CollectRewardsOfferView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _scoreText;
    [SerializeField, NotNull] private Button _collectRewardsButton;

    protected override void AwakeWindow()
    {
        _collectRewardsButton.onClick.AddListener(() => OnCollectRewards?.Invoke());
    }

    public event Action OnCollectRewards;

    public void Init(uint score)
    {
        SetScore(score);
    }

    private void SetScore(uint score)
    {
        _scoreText.SetText($"{score}");
    }
}
