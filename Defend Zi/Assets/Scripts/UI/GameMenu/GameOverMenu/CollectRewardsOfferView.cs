using Desdiene.UI.Elements;
using UnityEngine;

public class CollectRewardsOfferView : FullScreenWindow
{
    [SerializeField, NotNull] private TextView _scoreText;

    public void Init(int score)
    {
        SetScore(score);
    }

    private void SetScore(int score)
    {
        _scoreText.SetText($"Score: {score}");
    }
}
